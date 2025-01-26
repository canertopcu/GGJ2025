using UnityEngine;
using DG.Tweening;

public class AICharacterController : MonoBehaviour, IKnockback
{
    [Header("Sword Settings")]
    public Transform sword;
    public float swordAttackRangeMin = 1.0f;
    public float swordAttackRangeMax = 1.5f;
    public float swordAttackSpeed = 0.2f;
    public float swordReturnSpeed = 0.1f;

    [Header("Shield Settings")]
    public Transform shield;
    public float shieldAttackRangeMin = 0.5f;
    public float shieldAttackRangeMax = 1.0f;
    public float shieldAttackSpeed = 0.3f;
    public float shieldReturnSpeed = 0.15f;

    [Header("General Settings")]
    public float minAttackDelay = 1.0f;
    public float maxAttackDelay = 3.0f;

    private Vector3 initialSwordPosition;
    private Vector3 initialShieldPosition;
    private Tweener currentSwordTweener;
    private Tweener currentShieldTweener;

    private bool isGameActive;
    public bool isKnockback;
    float delay = 5f;
    void Start()
    {
        initialSwordPosition = sword.localPosition;
        initialShieldPosition = shield.localPosition;

        isGameActive = GameManager.Instance.GameState == GameState.Playing;

        if (isGameActive)
        {
            ScheduleNextAttack();
            ScheduleNextDefence();
        }
    }

    void Update()
    {
        // Oyun durumu değişirse hareketleri kontrol et
        if (GameManager.Instance.GameState == GameState.Playing)
        {
            if (!isGameActive)
            {
                isGameActive = true;
                ScheduleNextAttack();
                ScheduleNextDefence();
            }
        }
        else
        {
            if (isGameActive)
            {
                isGameActive = false;
                StopAllMovements();
            }
        }
    }

    private void ScheduleNextAttack()
    {
        if (isGameActive)
        {
            Invoke(nameof(PerformSwordAttack), delay);
            delay = Random.Range(minAttackDelay, maxAttackDelay);
        }
    }

    private void ScheduleNextDefence()
    {
        if (isGameActive)
        {
            float delay = Random.Range(minAttackDelay, maxAttackDelay);
            Invoke(nameof(PerformShieldAttack), delay);
        }
    }

    private void PerformSwordAttack()
    {
        if (!isGameActive) return;

        if (currentSwordTweener != null && currentSwordTweener.IsActive())
        {
            currentSwordTweener.Kill();
        }

        float attackRange = Random.Range(swordAttackRangeMin, swordAttackRangeMax);
        Vector2 randomDirection = Vector2.right * attackRange + Vector2.up * Random.Range(-1f, 1f);

        if (randomDirection.magnitude > attackRange)
        {
            randomDirection = randomDirection.normalized * attackRange;
        }

        Vector2 targetPosition = (Vector2)initialSwordPosition + randomDirection;

        currentSwordTweener = sword.DOLocalMove(
            new Vector3(targetPosition.x, targetPosition.y, initialSwordPosition.z),
            swordAttackSpeed
        ) 
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            currentSwordTweener = sword.DOLocalMove(
                new Vector3(initialSwordPosition.x, initialSwordPosition.y, initialSwordPosition.z),
                swordReturnSpeed
            )
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                ScheduleNextAttack();
            });
        });
    }

    private void PerformShieldAttack()
    {
        if (!isGameActive) return;

        if (currentShieldTweener != null && currentShieldTweener.IsActive())
        {
            currentShieldTweener.Kill();
        }

        float attackRange = Random.Range(shieldAttackRangeMin, shieldAttackRangeMax);
        Vector2 randomDirection = Vector2.right * attackRange + Vector2.up * Random.Range(-1f, 1f);

        if (randomDirection.magnitude > attackRange)
        {
            randomDirection = randomDirection.normalized * attackRange;
        }

        Vector2 targetPosition = (Vector2)initialShieldPosition + randomDirection;

        currentShieldTweener = shield.DOLocalMove(
            new Vector3(targetPosition.x, targetPosition.y, initialShieldPosition.z),
            shieldAttackSpeed
        )
        .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {
            currentShieldTweener = shield.DOLocalMove(
                new Vector3(initialShieldPosition.x, initialShieldPosition.y, initialShieldPosition.z),
                shieldReturnSpeed
            )
            .SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                ScheduleNextDefence();
            });
        });
    }

    private void StopAllMovements()
    {
        // Kılıç ve kalkan hareketlerini durdur
        if (currentSwordTweener != null)
        {
            currentSwordTweener.Kill();
        }
        if (currentShieldTweener != null)
        {
            currentShieldTweener.Kill();
        }

        CancelInvoke(nameof(PerformSwordAttack));
        CancelInvoke(nameof(PerformShieldAttack));
    }

    void OnDestroy()
    {
        StopAllMovements();
    }

    public void KnockBack()
    {
        Debug.LogError("gasda ");
    }
}
