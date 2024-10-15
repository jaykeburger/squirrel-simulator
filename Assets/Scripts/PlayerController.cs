using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

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
    private InputAction dashAction;
    private bool isDashing = false;
    private float dashCoolDownTimer; // Keep track of the time remain for dashing
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
    private Weapon[] weapons;  // Array of available weapons
    [SerializeField]
    private int currentWeapon = 0;

    // Audio clips for different actions
    [SerializeField]
    private AudioClip jumpClip;
    [SerializeField]
    private AudioClip shootClip;
    [SerializeField]
    private AudioClip moveClip;
    [SerializeField]
    private AudioClip sprintClip;
    
    // AudioSources for playing the clips
    private AudioSource jumpAudioSource;
    private AudioSource shootAudioSource;
    private AudioSource moveAudioSource;
    private AudioSource sprintAudioSource;

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
        dashAction = playerInput.actions["Dash"];
        currentSpeed = walkSpeed;

        // Create audio sources
        jumpAudioSource = gameObject.AddComponent<AudioSource>();
        shootAudioSource = gameObject.AddComponent<AudioSource>();
        moveAudioSource = gameObject.AddComponent<AudioSource>();
        sprintAudioSource = gameObject.AddComponent<AudioSource>();

        // Set the respective audio clips to the audio sources
        jumpAudioSource.clip = jumpClip;
        shootAudioSource.clip = shootClip;
        moveAudioSource.clip = moveClip;
        sprintAudioSource.clip = sprintClip;

    }
    private void OnEnable()
    {
        // Shooting
        shootAction.performed += _ =>
        {
            if (aimAction.IsPressed() && !PauseScript.GameIsPause) // Only activate shoot when aiming and Pause Menu is not active
            {
                weapons[currentWeapon].StartShooting(); // Shoot using the current weapon
                shootAudioSource.Play();
            }
        };
        shootAction.canceled += _ => weapons[currentWeapon].StopShooting();

        // Dashing
        dashAction.performed += _ => tryDash();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => weapons[currentWeapon].StartShooting();
        shootAction.canceled -= _ => weapons[currentWeapon].StopShooting();
        dashAction.performed -= _ => tryDash();
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
        Vector3 move = new Vector3(input.x, 0, input.y);

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
        
        if (isDashing)
        {
            PlayerState.Instance.UseStamina(Time.deltaTime, 50f);
            currentSpeed = dashSpeed;
        }
        // Make sure stamina bar only decrease if player move while hold down the shift button and on the ground
        else if (input != Vector2.zero && sprintAction.IsPressed() && PlayerState.Instance.currentStamina > 0 )
        {
            currentSpeed = sprintSpeed;
            PlayerState.Instance.UseStamina(Time.deltaTime, 10f); //Reduce stamina while sprinting
            if (!sprintAudioSource.isPlaying && !PauseScript.GameIsPause) // Play sprint sound only once when sprinting
            {
                sprintAudioSource.Play();
            }
        }
        else 
        {
            currentSpeed = walkSpeed;
            if (!sprintAction.IsPressed())
            {
                PlayerState.Instance.RecoverStamina(Time.deltaTime); //Recovery stamina if not sprinting
            }
            if (!moveAudioSource.isPlaying && input != Vector2.zero && !PauseScript.GameIsPause) // Play move sound when walking
            {
                moveAudioSource.Play();
            }
        }
        controller.Move(move * Time.deltaTime * currentSpeed);
        /*
        JUMPING=====================================================================================
        */
        // Changes the height position of the player.
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumpAudioSource.Play();
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

    private void tryDash()
    {
        // Restrain dashing from being active if at Jimmy's and Pause menu is actived
        if (!isDashing && dashCoolDownTimer <= 0f && SceneManager.GetActiveScene().name != "JimmyDorm" && !PauseScript.GameIsPause)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true; // Start Dashing
        dashCoolDownTimer = dashCoolDown; // Reset cooldown timer

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}