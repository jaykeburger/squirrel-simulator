using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableItem
{
    public GameObject itemPrefab; // The prefab of the item to spawn
    public int spawnRate; // Relative spawn rate of the item
}
public class WeaponSpawnManager : MonoBehaviour
{
    public List<SpawnableItem> spawnableItems; // List of all spawnable items
    public float spawnInterval = 5f; // Time in seconds between spawns
    public static WeaponSpawnManager Instance { get; private set; }

    void Start()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval); // Start immediately, repeat every 'spawnInterval' seconds
    }
    void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;

        }
        // DontDestroyOnLoad(gameObject);
    }   
    void SpawnItem()
    {
        if (spawnableItems.Count == 0)
            return;

        int totalRate = 0;
        foreach (var item in spawnableItems)
        {
            totalRate += item.spawnRate;
        }

        int randomNumber = Random.Range(0, totalRate);
        int currentSum = 0;

        foreach (var item in spawnableItems)
        {
            currentSum += item.spawnRate;
            if (randomNumber <= currentSum)
            {
                Vector3 baseSpawnPosition = new Vector3(108.769f, 1.5f, -64.68954f);
                float range = 100.0f;
                Vector3 spawnPosition = new Vector3(
                    baseSpawnPosition.x + Random.Range(-range, range),
                    baseSpawnPosition.y,
                    baseSpawnPosition.z + Random.Range(-range, range)
                );
                Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                break;
            }
        }
    }
}
