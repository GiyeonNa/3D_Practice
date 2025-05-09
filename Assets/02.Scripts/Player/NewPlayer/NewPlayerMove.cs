using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class NewPlayerMove : MonoBehaviour
{
    private CharacterController characterController;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.8f;
    public float mouseSensitivity = 100f; // Add sensitivity for mouse movement

    private Vector3 velocity;
    private bool isGrounded;
    private float rotationX = 0f; // Track vertical rotation

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure the GameObject has a CharacterController component
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle camera rotation for first-person view
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Apply vertical rotation
        transform.Rotate(Vector3.up * mouseX);

        // Check if the player is grounded
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset vertical velocity when grounded
        }

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);

        // Move relative to the camera's orientation
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0; // Keep movement on the horizontal plane
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
