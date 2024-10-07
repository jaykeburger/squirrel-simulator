using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction shootAction;
    private InputAction aimAction;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float walkSpeed = 8.0f;
    [SerializeField]
    private float sprintSpeed = 15.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private Weapon[] weapons;  // Array of available weapons
    [SerializeField]
    private int currentWeapon = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput= GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        currentSpeed = walkSpeed;
        print(weapons.Length);
    }
    private void OnEnable()
    {
        shootAction.performed += _ =>
        {
            if (aimAction.IsPressed()) // Only activate shoot when aiming
            {
                weapons[currentWeapon].StartShooting(); // Shoot using the current weapon
            }
        };

        shootAction.canceled += _ => weapons[currentWeapon].StopShooting();

    }

    private void OnDisable()
    {
        shootAction.performed -= _ => weapons[currentWeapon].StartShooting();
        shootAction.canceled -= _ => weapons[currentWeapon].StopShooting();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        //Moving according to the angel of the camera.
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        // Make sure stamina bar only decrease if player move while hold down the shift button and on the ground
        if (input != Vector2.zero && sprintAction.IsPressed() && PlayerState.Instance.currentStamina > 0 && groundedPlayer)
        {
            currentSpeed = sprintSpeed;
            PlayerState.Instance.UseStamina(Time.deltaTime); //Reduce stamina while sprinting
        }
        else 
        {
            currentSpeed = walkSpeed;
            if (!sprintAction.IsPressed())
            {
                PlayerState.Instance.RecoverStamina(Time.deltaTime); //Recovery stamina if not sprinting
            }
        }
        controller.Move(move * Time.deltaTime * currentSpeed);

        // Changes the height position of the player.
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //Rotate character with camera direction.
        if (input != Vector2.zero && !aimAction.IsPressed()) //Control player rotation with camera while not aiming
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        if (aimAction.IsPressed())
        {
            Quaternion rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        //Update when player switch weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !aimAction.IsPressed()) // If player scroll up and NOT aiming
        {
            if (currentWeapon >= weapons.Length - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon ++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !aimAction.IsPressed()) // If player scroll down
        {
            if (currentWeapon <=0)
            {
                currentWeapon = weapons.Length - 1;
            }
            else
            {
                currentWeapon --;
            }
        }
    }
}