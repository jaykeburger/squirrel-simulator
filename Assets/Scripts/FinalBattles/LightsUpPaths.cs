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
        path.GetComponent<MeshRenderer>().receiveShadows = false;
        pathOriginalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            showPath();
        }
        // if (MovingRats.shouldMove == false)
        // {
        //     path.GetComponent<MeshRenderer>().enabled = false;
        // }
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
            // Toggle visibility
            isVisible = !isVisible;
            meshRenderer.material.color = isVisible ? Color.red : Color.clear;

            // Wait for half a second
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.5f;
        }
        sphereActive = true;
        MovingRats.shouldMove = true;
    }
}
