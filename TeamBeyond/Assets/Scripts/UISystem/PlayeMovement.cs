using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController component
    public Transform cameraTransform;     // Reference to the camera's transform

    public float speed = 5f;              // Player movement speed
    public float gravity = -9.81f;        // Gravity value
    public float jumpHeight = 1.5f;       // Jump height (optional)

    private Vector3 velocity;             // For handling gravity
    private bool isGrounded;              // Ground check

    public Transform groundCheck;         // Transform to check if player is on the ground
    public float groundDistance = 0.4f;   // Radius for ground check
    public LayerMask groundMask;          // Layers considered as ground

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset gravity effect when grounded
        }

        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down

        // Calculate movement direction based on camera orientation
        Vector3 direction = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        direction.y = 0f; // Ensure no vertical movement

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction.normalized * speed * Time.deltaTime);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }
}
