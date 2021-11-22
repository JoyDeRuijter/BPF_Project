using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    private float groundDistance;

    private CharacterController controller;
    private Transform groundCheck;
    private Vector3 velocity;
    private bool isGrounded;

    public LayerMask groundMask;

    void Start()
    {
        speed = 12f;
        gravity = -30f;
        jumpHeight = 3f;
        groundDistance = 0.4f;
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
    }

    void Update()
    {
        HandlePhysics();
        PlayerMove();
        PlayerJump();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void HandlePhysics()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
