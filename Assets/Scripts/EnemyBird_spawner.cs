using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird_spawner : MonoBehaviour
{
    public GameObject birdPrefab;
    public float spawnInterval = 20f; // Set default to 20 seconds for slower spawn rate
    public float spawnHeight = 10f;

    void Start()
    {
        // Ensure any previous spawn invocations are cancelled before setting up a new one
        CancelInvoke("SpawnBird");
        InvokeRepeating("SpawnBird", 0f, spawnInterval);
    }

    void SpawnBird()
    {
        // Calculate a random spawn position within specified bounds
        Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), spawnHeight, Random.Range(-10, 10));
        
        // Log spawning for debugging purposes with timestamp
        Debug.Log("Spawning bird at: " + spawnPosition + " Time: " + Time.time);
        
        // Instantiate the bird prefab at the calculated position
        Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
    }
}
