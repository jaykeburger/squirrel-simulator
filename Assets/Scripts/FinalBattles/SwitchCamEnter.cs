using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEditor.Rendering;

public class SwitchCamEnter : MonoBehaviour
{
    public GameObject dialogCanvas;
    private int priorityBoostAmount = 20;
    private Cinemachine.CinemachineVirtualCamera vCam;
    private Quaternion initialRotation;
    private bool isTrigger = false;

    void Start()
    {
        if (InstanceZoomCam.Instance != null)
        {
            vCam =  InstanceZoomCam.Instance.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            initialRotation = vCam.transform.rotation;
        }
        else
        {
            Debug.Log("instance is null");
        }
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
        vCam.Priority += priorityBoostAmount;
        Transform child = dialogCanvas.transform.GetChild(0);

        yield return new WaitForSeconds(1f);
        if (child != null)
        {
            child.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(5f);

        child.gameObject.SetActive(false);
        vCam.Priority -= priorityBoostAmount;
    }
}

