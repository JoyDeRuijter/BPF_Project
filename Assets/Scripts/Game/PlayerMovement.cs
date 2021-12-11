using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed, gravity, jumpHeight;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameObject cowboy;

    private float groundDistance, capsuleHeight, walkSpeed, runSpeed;
    private bool isGrounded, isMoving, isCrouching, isWalking, isRunning, isJumping, isFalling;
    private CharacterController controller;
    private Animator anim;
    private CapsuleCollider capCollider;
    private Transform groundCheck;
    private Vector3 velocity, capsuleCenter;

    void Start()
    {
        walkSpeed = 12f;
        runSpeed = 20f;
        speed = walkSpeed;
        gravity = -30f;
        jumpHeight = 3.5f;
        groundDistance = 0.4f;
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
        anim = cowboy.GetComponent<Animator>();
        capCollider = cowboy.GetComponent<CapsuleCollider>();
        capsuleHeight = capCollider.height;
        capsuleCenter = capCollider.center;
    }

    void Update()
    {
        HandlePhysics();
        PlayerSpeed();
        PlayerMove();
        PlayerJump();
        UpdateAnimator();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
            isMoving = true;
        else
            isMoving = false;

        if (speed == walkSpeed && isMoving)
        {
            isWalking = true;
            isRunning = false;
        }
        else if (speed == runSpeed && isMoving)
        {
            isWalking = false;
            isRunning = true;
        }
        else if (!isMoving)
        {
            isWalking = false;
            isRunning = false;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void PlayerSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed = 20f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = 12f;
    }

    private void PlayerCrouch()
    { 
    
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (velocity.y > 2)
        {
            isJumping = true;
            isFalling = false;
        }
        else if (velocity.y <= 2 && velocity.y >= -1.99)
        {
            isJumping = false;
            isFalling = true;
        }
        else if (velocity.y <= -2)
        {
            isJumping = false;
            isFalling = false;
        }         
    }

    private void HandlePhysics()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsRunning", isRunning);
        anim.SetBool("IsCrouching", isCrouching);
        anim.SetBool("IsJumping", isJumping);
        anim.SetBool("IsFalling", isFalling);
    }
}
