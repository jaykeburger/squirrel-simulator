using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class LightsUpPaths : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject path;
    public float speed = 5f;
    private bool moveLeft = true;
    public static bool sphereActive = false;
    private Vector3 pathOriginalPos;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        path.GetComponent<MeshRenderer>().enabled = false;
        pathOriginalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            showPath();
        }
    }

    public void showPath()
    {
        path.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(LightsUp(3f));
    }

    IEnumerator LightsUp(float duration)
    {
        meshRenderer = path.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.Log("Null");
            yield break;
        }

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            // Toggle visibilityasd
            isVisible = !isVisible;
            meshRenderer.material.color = isVisible ? Color.red : Color.clear;

            // Wait for half a second
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.5f;
        }
        sphereActive = true;
        MovingRats.shouldMove = true;
        // MovePath();
    }

    public void MovePath()
    {
        transform.position = pathOriginalPos;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Found rb");
            rb.velocity = Vector3.zero; //Stop movement;
            rb.angularVelocity = Vector3.zero; //Stop rotation;
        }   
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (moveLeft)
        {
            transform.position += Vector3.left * 3f;
            moveLeft = false;
        }
        else
        {
            moveLeft = true;
        }
    }
}
