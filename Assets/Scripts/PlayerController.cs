using System.Collections;
using Unity.VisualScripting;
// using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
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
    private InputAction dashAction;
    private bool isDashing = false;
    private float dashCoolDownTimer; // Keep track of the time remain for dashing
    private int currentWeapon = 0;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration; 
    [SerializeField]
    private float dashCoolDown; //Time before player can dash again
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private BaseWeapon[] weapons;  // Array of available weapons
    public WeaponManager weaponManager;

    AudioManager audioManager;
    
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
        controller = GetComponent<CharacterController>();
        playerInput= GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        shootAction = playerInput.actions["Shoot"];
        aimAction = playerInput.actions["Aim"];
        dashAction = playerInput.actions["Dash"];
        currentSpeed = walkSpeed;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


    }
    private void OnEnable()
    {
        // Shooting
        if (SceneManager.GetActiveScene().name != "JimmyDorm")
        {
            shootAction.performed += OnShootPerformed;
            shootAction.canceled += OnShootingCanceled;

            // Dashing
            dashAction.performed += _ => tryDash();
        }
    }

    private void OnDisable()
    {
        shootAction.performed -= OnShootingCanceled;
        shootAction.canceled -= OnShootingCanceled;

        dashAction.performed -= _ => tryDash();
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (aimAction.IsPressed() && !PauseScript.GameIsPause && SceneManager.GetActiveScene().name != "JimmyDorm")
        {
            if (weaponManager != null && weaponManager.weapons.Length > 0)
            {
                weaponManager.StartShooting();
                audioManager.PlaySFX(audioManager.shootAcorn); 
            }
        }
    }

    private void OnShootingCanceled(InputAction.CallbackContext context)
    {
        if (weaponManager != null && weaponManager.weapons.Length > 0)
        {
            weaponManager.StopShooting();
        }
    }

    void Update()
    {
        /*
        MOVING WITH CAMERA===========================================================================
        */
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new(input.x, 0, input.y);

        //Moving according to the angel of the camera.
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        /*
        TIMER FOR DASHING============================================================
        */
        if (dashCoolDownTimer > 0f) //Reduce time left for dashing
        {
            dashCoolDownTimer -= Time.deltaTime;
        }

        /*
        SPRINTING, DASHING, AND STAMINA DECREASING============================================================
        */
        
        // Condition to only dash when still have stamina available
        if (isDashing && GlobalValues.currentStamina > 0)
        {
            // PlayerState.Instance.UseStamina(Time.deltaTime, 50f);
            currentSpeed = dashSpeed;
        }
        // Make sure stamina bar only decrease if player move while hold down the shift button and on the ground
        else if (input != Vector2.zero && sprintAction.IsPressed() && GlobalValues.currentStamina > 0 )
        {
            currentSpeed = sprintSpeed;
            PlayerState.Instance.UseStamina(Time.deltaTime, 10f); //Reduce stamina while sprinting
            if (!audioManager.SFXSource.isPlaying && !PauseScript.GameIsPause) // Play sprint sound only once when sprinting
            {
                audioManager.PlaySFX(audioManager.walk);
            }
        }
        else 
        {
            currentSpeed = walkSpeed;
            if (!sprintAction.IsPressed())
            {
                if(PlayerState.Instance != null)
                {
                    PlayerState.Instance.RecoverStamina(Time.deltaTime); //Recovery stamina if not sprinting
                }
            }
            if (!audioManager.SFXSource.isPlaying&& input != Vector2.zero && !PauseScript.GameIsPause) // Play move sound when walking
            {
                audioManager.PlaySFX(audioManager.walk);
            }
        }
        controller.Move(currentSpeed * Time.deltaTime * move);
        /*
        JUMPING=====================================================================================
        */
        // Changes the height position of the player.
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            audioManager.PlaySFX(audioManager.jump);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        /*
        CAMERA ROTATION=======================================================================================
        */
        //Rotate character with camera direction.
        if (input != Vector2.zero && !aimAction.IsPressed()) //Control player rotation with camera while not aiming
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        if (aimAction.IsPressed())
        {
            if (SceneManager.GetActiveScene().name == "JimmyDorm")
            {
                float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
        }
        /*
        SWITCHING WEAPONS FUNCTIONS===============================================================================
        */
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
            weaponManager.SwitchWeapon(currentWeapon);
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
            weaponManager.SwitchWeapon(currentWeapon);
        }
    }

    /*
    FUNCTIONS FOR DASHING===========================================================
    */
    private void tryDash()
    {
        if (this == null) Debug.Log("Object destroyed");
        // Restrain dashing from being active if at Jimmy's and Pause menu is actived
        if (!isDashing && dashCoolDownTimer <= 0f && SceneManager.GetActiveScene().name != "JimmyDorm" && !PauseScript.GameIsPause)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true; // Start Dashing
        PlayerState.Instance.UseStamina(Time.deltaTime, 50f);
        dashCoolDownTimer = dashCoolDown; // Reset cooldown timer

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        StopCoroutine(Dash());
    }
}