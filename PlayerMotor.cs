using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float sprintMultiplier = 1.5f; // Multiplier for sprint speed
    public bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 0.5f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void ProcessMove(Vector3 input)
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Check if Shift key is held for sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * sprintMultiplier : speed;

        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(transform.TransformDirection(moveDirection) * currentSpeed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
