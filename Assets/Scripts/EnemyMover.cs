using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // private GenerateEnemies enemyController;
    public Transform player;
    private Vector3 spawnPosition;

    // Variables for enemies patrolling
    public float patrolRange;//Patrol range from spawn position
    public float patrolSpeed;
    private bool movingRight = true;

    // Variables for enemies attacking
    public float attackRange;
    public float chaseSpeed;


    void Start()
    {
        spawnPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        // If touched player, then slow down.
        if (other.CompareTag("Player"))
        {
            chaseSpeed = 0;
            StartCoroutine(DelaySpeed());
        }
    }
    private IEnumerator DelaySpeed()
    {
        yield return new WaitForSeconds(3f);
        chaseSpeed = 10;
    } 
    
    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            Patrol();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void Patrol()
    {
        float leftBound = spawnPosition.x - patrolRange;
        float rightBound = spawnPosition.x + patrolRange;

        if (movingRight)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
            transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
            transform.position += Vector3.left * patrolSpeed * Time.deltaTime;
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
        transform.position += direction * chaseSpeed * Time.deltaTime;
    }
}
