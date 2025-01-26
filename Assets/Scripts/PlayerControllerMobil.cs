using UnityEngine;

public class PlayerControllerMobil : MonoBehaviour
{
    public Transform Sword;  // Sword objesi referansı
    public Transform Shield; // Shield objesi referansı

    public float radius = 2.5f;  // Yarıçap (hareket alanı sınırı)
    [Header("Movement Settings")]
    public float speed = 5f;  // Hareket hızı

    // Joystick referansları
    public Joystick leftJoystick;  // Sol joystick (Sword kontrolü için)
    public Joystick rightJoystick; // Sağ joystick (Shield kontrolü için)

    private Vector2 currentPositionSword;
    private Vector2 currentPositionShield;
    private Vector2 initialPositionSword;
    private Vector2 initialPositionShield;

    void Start()
    {
        // Başlangıç pozisyonlarını kaydet
        currentPositionSword = Sword.localPosition;
        currentPositionShield = Shield.localPosition;

        initialPositionSword = Sword.localPosition;
        initialPositionShield = Shield.localPosition;
    }

    void Update()
    {
        HandleSwordMovement();
        HandleShieldMovement();
        ReturnToInitialPosition();
    }

    private void HandleSwordMovement()
    {
        // Sol joystick girişini oku
        Vector2 movement = new Vector2(leftJoystick.Horizontal, leftJoystick.Vertical) * speed * Time.deltaTime;

        // Sword pozisyonunu güncelle
        currentPositionSword += movement;

        // Sword yarıçap sınırında kalmalı
        float distanceFromCenter = currentPositionSword.magnitude;
        if (distanceFromCenter > radius)
        {
            currentPositionSword = currentPositionSword.normalized * radius;
        }

        // Sadece pozitif eksende kal
        currentPositionSword.x = Mathf.Max(currentPositionSword.x, 0);
        currentPositionSword.y = Mathf.Max(currentPositionSword.y, 0);

        // Pozisyonu Sword'a uygula
        Sword.localPosition = new Vector3(currentPositionSword.x, currentPositionSword.y, Sword.localPosition.z);
    }

    private void HandleShieldMovement()
    {
        // Sağ joystick girişini oku
        Vector2 movement = new Vector2(rightJoystick.Horizontal, rightJoystick.Vertical) * speed * Time.deltaTime;

        // Shield pozisyonunu güncelle
        currentPositionShield += movement;

        // Shield yarıçap sınırında kalmalı
        float distanceFromCenter = currentPositionShield.magnitude;
        if (distanceFromCenter > radius)
        {
            currentPositionShield = currentPositionShield.normalized * radius;
        }

        // Sadece pozitif eksende kal
        currentPositionShield.x = Mathf.Max(currentPositionShield.x, 0);
        currentPositionShield.y = Mathf.Max(currentPositionShield.y, 0);

        // Pozisyonu Shield'a uygula
        Shield.localPosition = new Vector3(currentPositionShield.x, currentPositionShield.y, Shield.localPosition.z);
    }

    private void ReturnToInitialPosition()
    {
        // Sol joystick hareketsizse Sword başlangıç pozisyonuna döner
        if (leftJoystick.Horizontal == 0 && leftJoystick.Vertical == 0)
        {
            currentPositionSword = Vector2.MoveTowards(currentPositionSword, initialPositionSword, speed * Time.deltaTime);
            Sword.localPosition = new Vector3(currentPositionSword.x, currentPositionSword.y, Sword.localPosition.z);
        }

        // Sağ joystick hareketsizse Shield başlangıç pozisyonuna döner
        if (rightJoystick.Horizontal == 0 && rightJoystick.Vertical == 0)
        {
            currentPositionShield = Vector2.MoveTowards(currentPositionShield, initialPositionShield, speed * Time.deltaTime);
            Shield.localPosition = new Vector3(currentPositionShield.x, currentPositionShield.y, Shield.localPosition.z);
        }
    }
}
