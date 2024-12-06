using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBooks : MonoBehaviour
{
    public List<GameObject> bookList;
    public GameObject plane;
    public int spawnItemNum;
    // Start is called before the first frame update
    void Start()
    {
        if (plane == null)
        {
            Debug.Log("No plane found");
            return;
        }
        SpawingBooks();
    }

    // Update is called once per frame
    void Update()
    {
        if (CountBooks.bookCollected >= spawnItemNum)
        {
            CountBooks.bookCollected = 0;
            SpawingBooks();
        }
    }

    public void SpawingBooks()
    {
        for (int i = 0; i < spawnItemNum; i ++)
        {
            GameObject bookToSpawn = bookList[Random.Range(0, bookList.Count)];
            Vector3 spawnPos = GetRandomPos(plane);
            Instantiate(bookToSpawn, spawnPos, Quaternion.identity);
        }
    }

    public Vector3 GetRandomPos(GameObject plane)
    {
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer ==  null)
        {
            Debug.LogError("Plane GameObject must have a Renderer component.");
            return Vector3.zero;
        }
        Bounds bounds = planeRenderer.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 randomPos = new Vector3(randomX, bounds.max.y + 0.1f, randomZ);
        return randomPos;
    }
}
