using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header ("Player Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    [Header ("References")]
    [Space(10)]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameObject playerModel;

    private float groundDistance, walkSpeed, runSpeed;
    private bool isGrounded, isMoving, isWalking, isRunning, isJumping, isFalling;
    private CharacterController controller;
    private Animator anim;
    private Transform groundCheck;
    private Vector3 velocity;
    #endregion 

    void Awake()
    {
        walkSpeed = 12f;
        runSpeed = 20f;
        speed = walkSpeed;
        gravity = -30f;
        jumpHeight = 3.5f;
        groundDistance = 0.4f;
        controller = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("Player/GroundCheck").GetComponent<Transform>();
        anim = playerModel.GetComponent<Animator>();
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

        //Check if the there is any input and therefore if the player should move
        if (x != 0 || z != 0)
            isMoving = true;
        else
            isMoving = false;

        //Determine whether the player is walking or running using the speed value
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

        //Transform the player position using the directions and speed
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

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //Determine whether the player is jumping or falling using the velocity
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
        //Use a groundcheck sphere to check if the player is on the ground
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
        anim.SetBool("IsJumping", isJumping);
        anim.SetBool("IsFalling", isFalling);
    }
}
