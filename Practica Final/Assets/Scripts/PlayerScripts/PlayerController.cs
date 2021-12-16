using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public AudioClip pickupClip;

    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float animSmoothTime = 0.05f;
    [SerializeField] private float animationPlayTransition = 0.15f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform camTransform;

    private PlayerStats playerStats;

    private InputAction moveAction;
    private InputAction jumpAction;

    private Animator animator;
    private int jumpAnimParam;
    private int moveXAnimParam;
    private int moveZAnimParam;

    private Vector2 currentBlend;
    private Vector2 animVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
        camTransform = Camera.main.transform;

        // Input system references
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        // Lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Animtions
        animator = GetComponent<Animator>();
        jumpAnimParam = Animator.StringToHash("Pistol Jump");
        moveXAnimParam = Animator.StringToHash("MoveX");
        moveZAnimParam = Animator.StringToHash("MoveZ");
    }

    void FixedUpdate()
    {
        if (!playerStats.isDead)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = moveAction.ReadValue<Vector2>();
            currentBlend = Vector2.SmoothDamp(currentBlend, input, ref animVelocity, animSmoothTime);
            Vector3 move = new Vector3(currentBlend.x, 0, currentBlend.y);
            move = move.x * camTransform.right.normalized + move.z * camTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            // Blend Animations
            animator.SetFloat(moveXAnimParam, currentBlend.x);
            animator.SetFloat(moveZAnimParam, currentBlend.y);

            // Changes the height position of the player..
            if (jumpAction.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.CrossFade(jumpAnimParam, animationPlayTransition);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // Rotate towards cam direction
            Quaternion targetRotation = Quaternion.Euler(0, camTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}