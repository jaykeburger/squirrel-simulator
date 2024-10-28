using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount = 10;
    [SerializeField]
    private Canvas aimCanvas;
    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    private static bool aimCanvasExists = false;


    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        if (aimCanvas != null && !aimCanvasExists)
        {
            aimCanvas.transform.SetParent(null);
            DontDestroyOnLoad(aimCanvas.gameObject);
            aimCanvasExists = true;
        }
    }

    private void OnEnable()
    {
            aimAction.performed += _ => StartAim();
            aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        if (!PauseScript.GameIsPause && SceneManager.GetActiveScene().name != "JimmyDorm") {
            virtualCamera.Priority += priorityBoostAmount; //Boost the priority of aiming camera.
            if (aimCanvas  == null)
            {
                Debug.Log("canvas is Null");
            }
            else
            {
                aimCanvas.enabled = true;
            }
            // aimCanvas.enabled = true;
        }
    }

    private void CancelAim()
    {
        if (!PauseScript.GameIsPause && SceneManager.GetActiveScene().name != "JimmyDorm") {
            virtualCamera.Priority -= priorityBoostAmount;
            if (aimCanvas  == null)
            {
                Debug.Log("canvas is Null");
            }
            else
            {
                aimCanvas.enabled = false;
            }
            // aimCanvas.enabled = false;
        }
    }
}
