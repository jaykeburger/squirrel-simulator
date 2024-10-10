using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    public void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 15)
        {
            xPos = Random.Range(0, 215);
            zPos = Random.Range(-180, 55);
            Instantiate(enemy, new Vector3(xPos, 0.6f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            enemyCount += 1;
        }
    }
}
