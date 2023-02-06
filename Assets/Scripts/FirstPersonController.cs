using System;
using System.Collections;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;

    public bool isSprinting => canSprint && Input.GetKey(sprintKey);

    public bool shouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;


    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canHeadBob = true;
    [SerializeField] private bool canHeal = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode healKey = KeyCode.E;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeeed = 6.0f;
    [SerializeField] private float sprintSpeeed = 12.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField, ] private float jumpForce = 8.0f;
    [SerializeField, ] private float gravity = 30.0f;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;

    float defaultYPos = 0;
    float timer = 0;

    [Header("Health Parameters")]
    [SerializeField] private int maxArrowCount = 3;
    [SerializeField] private float timeToHeal = 0.05f;
    private int currentArrowCount = 0;
    private Coroutine regeneratingHealth;
    public static Action<float> OnTakeDamage;

    [Header ("Stamina Parameters")]


    float time;
    private Camera playerCamera;
    private CharacterController characterController;
    private float rotationX = 0;
    private Vector3 moveDirection;
    private Vector3 moveRotation = new Vector3(0, 0, 0);
    private Vector2 currentInput;



    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultYPos = playerCamera.transform.localPosition.y;

    }
    void Update()
    {
        if (CanMove)
        {
            MovementInput();

            CameraMovement();

            if (canJump)
                HandleJump();

            if (canHeadBob)
                HandleHeadBob();

            if (canHeal)
                HandleHeal();

            ApplyFinalMovements();
        }
    }

    IEnumerator RegenerateHealth()
    {
        if (currentArrowCount <- 0)
                yield break;

        if (Input.GetKey(healKey))
        {
            if (time < timeToHeal)
            {
                time += Time.deltaTime;
                yield return new WaitForSeconds(timeToHeal);
                
            }
            currentArrowCount--;
            time = 0.0f;
        }
        regeneratingHealth = null;
    }
    void HandleHeal()
    {
        if(regeneratingHealth != null)
            StopCoroutine(RegenerateHealth());

        regeneratingHealth = StartCoroutine(RegenerateHealth());
    }


    void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

    }

    void HandleJump()
    {
        if (shouldJump)
            moveDirection.y = jumpForce;
    }

    void HandleHeadBob()
    {
        if (!characterController.isGrounded)
        {
            return;
        }
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
        else
        {
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos,
                playerCamera.transform.localPosition.z);
        }
    }


    void MovementInput()
    {
    
        currentInput = new Vector2((isSprinting ? sprintSpeeed : walkSpeeed) * Input.GetAxis("Vertical"),
                                  (isSprinting ? sprintSpeeed : walkSpeeed) * Input.GetAxis("Horizontal"));

            float moveDirectionY = moveDirection.y;
            moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) +
                            (transform.TransformDirection(Vector3.right) * currentInput.y);

            moveDirection.y = moveDirectionY;
    }

    void CameraMovement()
    {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);


    }
}
