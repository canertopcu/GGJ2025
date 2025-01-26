using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform Sword;
    public Transform Shield;

    public float radius = 2.5f; // Radius of the semi-circle
    [Header("Movement Settings")]
    public float speed = 5f; // Movement speed
    private Vector2 currentPositionSword;
    private Vector2 currentPositionShield;
    private Vector2 initialPositionSword;
    private Vector2 initialPositionShield;

    void Start()
    {
        // Initialize the current positions to the object's starting positions
        currentPositionSword = Sword.localPosition;
        currentPositionShield = Shield.localPosition;

        // Store initial positions
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
        // Get input from WASD keys
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Calculate the desired movement
        Vector2 movement = new Vector2(inputX, inputY) * speed * Time.deltaTime;

        // Update the current position incrementally
        currentPositionSword += movement;

        // Ensure the sword stays within the semi-circle boundary
        float distanceFromCenter = currentPositionSword.magnitude;
        if (distanceFromCenter > radius)
        {
            currentPositionSword = currentPositionSword.normalized * radius;
        }

        // Ensure the sword stays in the positive x and y axis
        currentPositionSword.x = Mathf.Max(currentPositionSword.x, 0);
        currentPositionSword.y = Mathf.Max(currentPositionSword.y, 0);

        // Apply the position to the transform
        Sword.localPosition = new Vector3(currentPositionSword.x, currentPositionSword.y, Sword.localPosition.z);
    }

    private void HandleShieldMovement()
    {
        // Get input from arrow keys
        float inputX = Input.GetAxis("ArrowHorizontal");
        float inputY = Input.GetAxis("ArrowVertical");

        // Calculate the desired movement
        Vector2 movement = new Vector2(inputX, inputY) * speed * Time.deltaTime;

        // Update the current position incrementally
        currentPositionShield += movement;

        // Ensure the shield stays within the semi-circle boundary
        float distanceFromCenter = currentPositionShield.magnitude;
        if (distanceFromCenter > radius)
        {
            currentPositionShield = currentPositionShield.normalized * radius;
        }

        // Ensure the shield stays in the positive x and y axis
        currentPositionShield.x = Mathf.Max(currentPositionShield.x, 0);
        currentPositionShield.y = Mathf.Max(currentPositionShield.y, 0);

        // Apply the position to the transform
        Shield.localPosition = new Vector3(currentPositionShield.x, currentPositionShield.y, Shield.localPosition.z);
    }

    private void ReturnToInitialPosition()
    {
        // Check if no input is given for both Sword and Shield
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            currentPositionSword = Vector2.MoveTowards(currentPositionSword, initialPositionSword, speed * Time.deltaTime);
            Sword.localPosition = new Vector3(currentPositionSword.x, currentPositionSword.y, Sword.localPosition.z);
        }

        if (Input.GetAxis("ArrowHorizontal") == 0 && Input.GetAxis("ArrowVertical") == 0)
        {
            currentPositionShield = Vector2.MoveTowards(currentPositionShield, initialPositionShield, speed * Time.deltaTime);
            Shield.localPosition = new Vector3(currentPositionShield.x, currentPositionShield.y, Shield.localPosition.z);
        }
    }
}
