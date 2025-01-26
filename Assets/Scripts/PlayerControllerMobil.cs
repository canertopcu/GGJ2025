using UnityEngine;

public class PlayerControllerMobil : MonoBehaviour, IKnockback
{
    public Transform Sword;  // Sword objesi referansı
    public Transform Shield; // Shield objesi referansı

    public float radius = 2.5f;  // Yarıçap (hareket alanı sınırı)
    [Header("Movement Settings")]
    public float speed = 5f;  // Hareket hızı

    public Joystick leftJoystick;  // Sol joystick (Sword kontrolü için)
    public Joystick rightJoystick; // Sağ joystick (Shield kontrolü için)

    private Vector2 currentPositionSword;
    private Vector2 currentPositionShield;
    private Vector2 initialPositionSword;
    private Vector2 initialPositionShield;

    private float inputPauseTimer = 0f;  // Input duraklama süresi için zamanlayıcı
    public float inputPauseDuration = 0.5f; // Input duraklama süresi (örneğin, 0.5 saniye)
    public bool isKnockback = false;
    void Start()
    {
        currentPositionSword = Sword.localPosition;
        currentPositionShield = Shield.localPosition;

        initialPositionSword = Sword.localPosition;
        initialPositionShield = Shield.localPosition;
    }

    void Update()
    {

        if (GameManager.Instance.GameState != GameState.Playing)
            return;
        // Eğer input duraklaması aktif değilse hareketi işleme
        if (!isKnockback)
        { 
            HandleSwordMovement();
            HandleShieldMovement();
        }
       

        ReturnToInitialPosition();

        if (isKnockback && inputPauseTimer >= 0)
        {
            inputPauseTimer -= Time.deltaTime;
        }
        else if (isKnockback){
            isKnockback = false;
            inputPauseTimer = inputPauseDuration;
        }
    }

    private void HandleSwordMovement()
    {
        Vector2 movement = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical) * speed * Time.deltaTime;
        currentPositionSword += movement;

        float distanceFromCenter = currentPositionSword.magnitude;
        if (distanceFromCenter > radius)
        {
            currentPositionSword = currentPositionSword.normalized * radius;
        }

        currentPositionSword.x = Mathf.Max(currentPositionSword.x, 0);
        currentPositionSword.y = Mathf.Max(currentPositionSword.y, 0);

        Sword.localPosition = new Vector3(currentPositionSword.x, currentPositionSword.y, Sword.localPosition.z);
    }

    private void HandleShieldMovement()
    {
        Vector2 movement = new Vector2(rightJoystick.Horizontal, rightJoystick.Vertical) * speed * Time.deltaTime;
        currentPositionShield += movement;

        float distanceFromCenter = currentPositionShield.magnitude;
        if (distanceFromCenter > radius)
        {
            currentPositionShield = currentPositionShield.normalized * radius;
        }

        currentPositionShield.x = Mathf.Max(currentPositionShield.x, 0);
        currentPositionShield.y = Mathf.Max(currentPositionShield.y, 0);

        Shield.localPosition = new Vector3(currentPositionShield.x, currentPositionShield.y, Shield.localPosition.z);
    }

    private void ReturnToInitialPosition()
    {
        if ((leftJoystick.Horizontal == 0 && leftJoystick.Vertical == 0) || isKnockback)
        {
            currentPositionSword = Vector2.MoveTowards(currentPositionSword, initialPositionSword, speed * Time.deltaTime);
            Sword.localPosition = new Vector3(currentPositionSword.x, currentPositionSword.y, Sword.localPosition.z);
        }

        if ((rightJoystick.Horizontal == 0 && rightJoystick.Vertical == 0) || isKnockback)
        {
            currentPositionShield = Vector2.MoveTowards(currentPositionShield, initialPositionShield, speed * Time.deltaTime);
            Shield.localPosition = new Vector3(currentPositionShield.x, currentPositionShield.y, Shield.localPosition.z);
        }
    }

    // Kılıç hareketini duraklatan metot
    public void PauseInput()
    {
        inputPauseTimer = inputPauseDuration;
    }

    public void KnockBack()
    { 
        isKnockback = true;
    }
}
