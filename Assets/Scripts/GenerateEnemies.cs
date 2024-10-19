using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public int maxEnemies;
    public int enemyCount;
    public int spawnRadius;
    private List<GameObject> enemyPool = new();
    private Dictionary<GameObject, Vector3> enemySpawnMap = new();
    public List<Vector3> spawnPositions =  new();
    private int xPos;
    private int zPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (true) //Constanly check if number of active enemy on the scene is less than 5 to reactive it at new position
        {
            while(enemyCount < maxEnemies)
            {
                GameObject enemy = getEnemyFromPool(); // Get an inactive enemy from the pool to active them.
                Vector3 randomSpawnPosition = getRandomPosition(); // Get a random position for enemy
                enemy.transform.position = randomSpawnPosition;
                enemy.SetActive(true); // Set it active on the scene.
                enemySpawnMap[enemy] = randomSpawnPosition;
                yield return new WaitForSeconds(1f); // Wait for 3s for each enemy to be spawned
                enemyCount += 1;
            }
            yield return new WaitForSeconds(10f); // Wait for 10s to reactivate enemy
        }
    }

    private GameObject getEnemyFromPool()
    {
        foreach (GameObject enemyInList in enemyPool)
        {
            if (!enemyInList.activeInHierarchy)
            {
                return enemyInList; //If find an inactive enemy, the return it EnemyDrop() to active it.
            }
        }

        // If no inactive enemy found in the list, then create one.
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false); //Initiate them but not active them on the scene.
        enemyPool.Add(enemy);
        return enemy;
    }

    private Vector3 getRandomPosition()
    {
        Vector3 spawnPosition;
        bool validPos = false;
        do 
        {
            xPos = Random.Range(0, 215);
            zPos = Random.Range(-180, 55);
            spawnPosition = new Vector3 (xPos, 0.6f, zPos);
            validPos = true;
            foreach (Vector3 pos in spawnPositions)
            {
                // Make sure each enemy spawn with distance between them and far from player when spawn (attack range is 30, change if needed)
                if (Vector3.Distance(spawnPosition, pos)< spawnRadius 
                    && Vector3.Distance(spawnPosition, player.position) < 30) 
                {
                    validPos = false;
                    break; //Stop loop through each position and generate a new position.
                }
            }
        } while (!validPos);
        
        spawnPositions.Add(spawnPosition);
        return spawnPosition;
    }

    public void removeEnemyPosition(GameObject enemyToRemoved)
    {
        Vector3 removePosition = enemySpawnMap[enemyToRemoved]; // Get the spawn position
        spawnPositions.Remove(removePosition); // Remove that position from the list
        
        // Remove the position value at that enemy key. Not delete the whole object.
        enemySpawnMap[enemyToRemoved] = Vector3.zero;
        enemyCount -= 1;
    }
}
