using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircleDrawer : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject spherePrefab;
    public GameObject plane;
    public int maxCircle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InitializeCircles();
        }
    }

    public void InitializeCircles()
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer ==  null)
        {
            Debug.LogError("Plane GameObject must have a Renderer component.");
            return;
        }
        Bounds bounds = planeRenderer.bounds;
        for (int i = 0; i < maxCircle; i ++)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 randomPos = new Vector3(randomX, bounds.max.y + 0.1f, randomZ);


            GameObject circle = Instantiate(circlePrefab, randomPos, Quaternion.identity);
            circle.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Rotate to lie flat

            //Add color red
            SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red; // Change color to red
            }

            // Start blinking effect for 3s
            StartCoroutine(BlinkCircle(circle, 3f));
        }
    }

    IEnumerator BlinkCircle(GameObject circle, float duration)
    {
        SpriteRenderer spriteRenderer = circle.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) yield break;

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            // Toggle visibility
            isVisible = !isVisible;
            spriteRenderer.color = isVisible ? Color.red : Color.clear;

            // Wait for half a second
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
        }

        // Ensure the circle remains red at the end
        spriteRenderer.color = Color.red;
        // Instantiate(spherePrefab, circle.transform.position + new Vector3(0, 10f, 0), Quaternion.identity);
        yield return StartCoroutine(SpawnSpheres(circle.transform.position, 3f));
        yield return new WaitForSeconds(2.9f);
        Destroy(circle);
    }

    IEnumerator SpawnSpheres(Vector3 spawnPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Spawn a sphere at the specified position
            Instantiate(spherePrefab, spawnPosition + new Vector3(0, 60f, 0), Quaternion.identity);

            // Wait for 0.3 seconds before spawning the next sphere
            yield return new WaitForSeconds(0.3f);
            elapsedTime += 0.3f;
        }
    }
}
