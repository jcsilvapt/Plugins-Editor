using UnityEngine;

public class FPSController : MonoBehaviour
{
     [Header("Camera Settings")]
    public Vector2 verticalAngleLimit = new Vector2(-89f, 89f);

    [Header("Movement Settings")]
    public float rotationSpeed = 200f;
    public float maxSpeedOnGround = 10f;
    public float MovementSharpnessOnGround = 15f;
    public float sprintModifier = 1.5f;

    [Header("Physics Settings")]
    public float gravity = -9.81f;
    public float groundGravity = -2f;
    public bool isGrounded;

    [Header("References")]
    public CharacterController characterController;
    public Transform playerCamera;

    [Header("Debugger")]
    public bool hideMouseOnStart = false;
    public InputManager inputManager;

    private float c_CameraVerticalAngle = 0f;
    private Vector3 c_CharacterVelocity = Vector3.zero;
    private Vector3 verticalVelocity = Vector3.zero;
    private bool isMoving = false;

    void Awake()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (playerCamera == null) playerCamera = GetComponent<Transform>();
    }

    void Start()
    {
        this.inputManager = InputManager.Instance;
        if (hideMouseOnStart)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Check Rotation
        {
            CheckCharacterRotation();
        }

        // Vertical Camera Rotation
        {
            CheckVerticalCameraRotation();
        }

        // Player Movement
        {
            CheckCharacterMovement();
        }

        {
            CheckForInteractable();
        }
    }

    void CheckCharacterRotation() {
        // Get Mouse X Input
        transform.Rotate(new Vector3(0f, inputManager.GetMouseMovementInput().x * rotationSpeed, 0f), Space.Self);
    }
    void CheckVerticalCameraRotation() {
        // Get Mouse Y Input
        float mouseYInput = inputManager.GetMouseMovementInput().y;

        // Add Vertical inputs
        c_CameraVerticalAngle += mouseYInput * rotationSpeed;

        // Limit Vertical Angles
        c_CameraVerticalAngle = Mathf.Clamp(c_CameraVerticalAngle, verticalAngleLimit.x, verticalAngleLimit.y);

        // Apply vertical Angle
        playerCamera.transform.localEulerAngles = new Vector3(c_CameraVerticalAngle, 0, 0);
    }

    void CheckCharacterMovement() {
        // Get Movement Inputs
        Vector2 movement = inputManager.GetMovementInput();
        if (movement == Vector2.zero) {
            isMoving = false;
        } else {
            isMoving = true;
        }
        // Get Movement inputs
        Vector3 worldSpaceMoveInput = transform.TransformVector(new Vector3(movement.x, 0f, movement.y));

        // Get Sprint Input
        //bool isSprinting = inputManager.GetSprintInput();

        // Check sprint velocity
        //float speedModifier = isSprinting ? sprintModifier : 1f;

        // Calculate Velocity
        Vector3 targetVelocity = worldSpaceMoveInput * maxSpeedOnGround * 1f;

        // Smooth movement
        c_CharacterVelocity = Vector3.Lerp(c_CharacterVelocity, targetVelocity, MovementSharpnessOnGround * Time.deltaTime);

        // Physics
        isGrounded = characterController.isGrounded;

        if (isGrounded && verticalVelocity.y < 0f) {
            verticalVelocity.y = groundGravity;
        } else {
            verticalVelocity.y += gravity * Time.deltaTime;
        }
        // Move Character
        characterController.Move((c_CharacterVelocity + verticalVelocity) * Time.deltaTime);
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
    }

}
