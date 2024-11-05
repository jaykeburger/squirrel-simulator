using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBirdEnemies : MonoBehaviour
{
    public GameObject birdPrefab;  // Reference to the bird enemy prefab
    public Transform player;
    public int maxBirds;  // Max number of birds that can be active at once
    public int birdCount;  // Current number of active birds
    public int spawnRadius;
    public float spawnHeight = 10f;  // Height at which birds spawn
    private List<GameObject> birdPool = new();
    private Dictionary<GameObject, Vector3> birdSpawnMap = new();
    public List<Vector3> spawnPositions = new();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(BirdDrop());
    }

    IEnumerator BirdDrop()
    {
        while (true) // Constantly checks if the number of active birds is less than maxBirds to reactivate them at a new position
        {
            while (birdCount < maxBirds)
            {
                GameObject bird = getBirdFromPool();  // Get an inactive bird from the pool
                Vector3 randomSpawnPosition = getRandomPosition();  // Get a random position for bird
                bird.transform.position = randomSpawnPosition;
                bird.SetActive(true);  // Set it active on the scene
                birdSpawnMap[bird] = randomSpawnPosition;
                yield return new WaitForSeconds(1f);  // Wait for 1s for each bird to be spawned
                birdCount += 1;
            }
            yield return new WaitForSeconds(10f);  // Wait for 10s to reactivate bird
        }
    }

    private GameObject getBirdFromPool()
    {
        foreach (GameObject birdInList in birdPool)
        {
            if (!birdInList.activeInHierarchy)
            {
                return birdInList;  // Return an inactive bird to activate it
            }
        }

        // If no inactive bird found in the list, then create one
        GameObject bird = Instantiate(birdPrefab);
        bird.SetActive(false);  // Initiate them but do not activate them on the scene
        birdPool.Add(bird);
        return bird;
    }

    private Vector3 getRandomPosition()
    {
        Vector3 spawnPosition;
        bool validPos = false;
        do 
        {
            float xPos = Random.Range(-100, 100);  // Customize range as needed
            float zPos = Random.Range(-100, 100);  // Customize range as needed
            spawnPosition = new Vector3(xPos, spawnHeight, zPos);
            validPos = true;
            foreach (Vector3 pos in spawnPositions)
            {
                // Make sure each bird spawns with enough distance between them and is far from player
                if (Vector3.Distance(spawnPosition, pos) < spawnRadius && Vector3.Distance(spawnPosition, player.position) < 50)
                {
                    validPos = false;
                    break;  // Generate a new position if the current one is invalid
                }
            }
        } while (!validPos);

        spawnPositions.Add(spawnPosition);
        return spawnPosition;
    }

    public void removeBirdPosition(GameObject birdToRemove)
    {
        Vector3 removePosition = birdSpawnMap[birdToRemove];  // Get the spawn position
        spawnPositions.Remove(removePosition);  // Remove that position from the list
        birdSpawnMap[birdToRemove] = Vector3.zero;
        birdCount -= 1;
    }
}
