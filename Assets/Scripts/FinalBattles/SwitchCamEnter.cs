using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEditor.Rendering;

/// <summary>
/// This script is no work. Need to improve somehow
/// </summary>
public class SwitchCamEnter : MonoBehaviour
{
    private Camera mainCamera;
    public GameObject dialogCanvas;
    private int priorityBoostAmount = 20;
    private Cinemachine.CinemachineVirtualCamera vCam;
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private bool isTrigger = false;
    public Vector3 hardcodedPosition = new Vector3(350f, 4.87f, -396.9f);
    public Quaternion hardcodedRotation = Quaternion.Euler(6.6f, 90.39f, 0f);

    void Start()
    {
        if (InstanceZoomCam.Instance != null)
        {
            vCam =  InstanceZoomCam.Instance.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            initialRotation = vCam.transform.rotation;
            initialPosition = vCam.transform.position;
        }
        else
        {
            Debug.Log("instance is null");
        }
        mainCamera = Camera.main;
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player") && !isTrigger)
        {
            isTrigger = true;
            if (vCam != null)
            {
                Debug.Log("Player Enter");
                StartCoroutine(ZoomIn());
            }
        }
    }

    IEnumerator ZoomIn()
    {
        // Increase priority to make this camera active
        vCam.Priority += priorityBoostAmount;

        // Set hardcoded position and rotation
        // vCam.transform.position = new Vector3(255.58f, 20.89f, -398.65f);
        // vCam.transform.rotation = new Quaternion(0.07509f, 0.68906f, -0.07214f, 0.71718f);

        // Force the main camera to match the virtual camera's position and rotation
        // yield return new WaitForEndOfFrame();
        // SyncMainCameraWithVirtualCamera();

        // Wait for 1 second
        // yield return new WaitForSeconds(1f);

        // Show dialog canvas
        Transform child = dialogCanvas.transform.GetChild(0);
        if (child != null)
        {
            child.gameObject.SetActive(true);
        }

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Hide dialog canvas
        if (child != null)
        {
            child.gameObject.SetActive(false);
        }

        // Reset camera priority
        vCam.Priority -= priorityBoostAmount;
        // isTrigger = false;

        // Debug.Log("Zoom completed with synchronized transforms.");
    }

    private void SyncMainCameraWithVirtualCamera()
    {
        if (mainCamera != null && vCam != null)
        {
            mainCamera.transform.position = new Vector3(255.58f, 20.89f, -398.65f);
            mainCamera.transform.rotation = new Quaternion(0.07509f, 0.68906f, -0.07214f, 0.71718f);
            Debug.Log("Main camera synchronized with virtual camera.");
        }
        else
        {
            Debug.LogWarning("Main camera or virtual camera is null!");
        }
    }
}

