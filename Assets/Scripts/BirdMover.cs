// using UnityEngine;

// public class BirdMover : MonoBehaviour
// {
//     public Transform target;
//     public float speed = 5f;
//     public float diveSpeed = 20f;
//     public float chaseDistance = 30f;
//     public float circleRadius = 10f;
//     public float chaseTime = 5f;  // Time to chase before diving
//     public float cooldownTime = 3f;  // Cooldown time after attacking before chasing again
//     public Vector3[] waypoints;
//     public AudioClip diveAttackClip; // Dive attack sound clip

//     private int currentWaypointIndex = 0;
//     private float chaseTimer = 0;
//     private float cooldownTimer = 0;
//     private bool isDiving = false;
//     private bool isCoolingDown = false;
//     private AudioSource audioSource; // Audio source for playing dive sound

//     private void Start()
//     {
//         if (target == null)
//         {
//             target = GameObject.FindGameObjectWithTag("Player").transform;
//         }
//         if (waypoints.Length == 0)
//         {
//             waypoints = new Vector3[] {
//                 new Vector3(50, 10, 50),
//                 new Vector3(-50, 15, -50),
//                 new Vector3(100, 20, 100),
//                 new Vector3(-100, 10, -100)
//             };
//         }
//          // Initialize the AudioSource for dive attack sound
//         audioSource = gameObject.AddComponent<AudioSource>();
//         audioSource.clip = diveAttackClip;
//         audioSource.playOnAwake = false;
//     }

//     private void Update()
//     {
//         if (isCoolingDown)
//         {
//             Patrol();
//             cooldownTimer += Time.deltaTime;
//             if (cooldownTimer >= cooldownTime)
//             {
//                 isCoolingDown = false;
//                 cooldownTimer = 0;
//             }
//             return;  // Skip other behaviors during cooldown
//         }

//         if (isDiving)
//         {
//             DiveAttack();
//             return;
//         }

//         float distance = Vector3.Distance(transform.position, target.position);
//         if (distance < chaseDistance && !isDiving && !isCoolingDown)
//         {
//             ChasePlayer();
//         }
//         else
//         {
//             Patrol();
//             chaseTimer = 0;
//         }
//     }

//     void Patrol()
//     {
//         if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 1f)
//         {
//             currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
//         }
//         Vector3 direction = (waypoints[currentWaypointIndex] - transform.position).normalized;
//         transform.position += direction * speed * Time.deltaTime;
//     }

//     void ChasePlayer()
//     {
//         Vector3 flatTargetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
//         Vector3 direction = (flatTargetPosition - transform.position).normalized;
//         transform.position += direction * speed * Time.deltaTime;
//         chaseTimer += Time.deltaTime;

//         if (chaseTimer >= chaseTime)
//         {
//             isDiving = true;
//             chaseTimer = 0;
//         }
//         // Play the dive attack sound when starting the dive
//         if (audioSource != null && audioSource.clip != null)
//         {
//                 audioSource.Play();
//         }
//     }

//     void DiveAttack()
//     {
//         Vector3 direction = (target.position - transform.position).normalized;
//         transform.position += direction * diveSpeed * Time.deltaTime;

//         if (Vector3.Distance(transform.position, target.position) < 2f)
//         {
//             isDiving = false;
//             isCoolingDown = true;
//             currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdMover : MonoBehaviour
{
    public Transform target;
    public float speed = 7f;
    public float diveSpeed = 20f;
    public float chaseDistance = 50f;
    public float circleRadius = 10f;
    public float chaseTime = 5f;  // Time to chase before diving
    public float cooldownTime = 3f;  // Cooldown time after attacking before chasing again
    public float diveDuration = 5f; // Maximum duration of dive attack
    public float patrolTimer = 0f;
    public float patrolDuration = 30f;
    public bool isPatrolling = true;
    public Queue<Vector3> waypoints = new();
    // public Vector3[] waypoints;
    private Vector3 currentWaypoint;
    public int maxWaypoints = 10;
    public float xMin = 0, xMax = 215f;
    public float zMin = -180f, zMax = 55f;
    private float chaseTimer = 0;
    private float cooldownTimer = 0;
    private float diveTimer = 0; // Timer to track the duration of the dive attack
    private bool isDiving = false;
    private bool isCoolingDown = false;
    private AudioSource audioSource; // Audio source for playing dive sound

    public int randIdx;

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        audioSource = GetComponent<AudioSource>();
        InitializeWaypoints();
        SetNextWaypoint();
    }

    private void Update()
    {
        if (isCoolingDown)
        {
            Patrol();
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                isCoolingDown = false;
                cooldownTimer = 0;
            }
            return;
        }

        if (isDiving)
        {
            DiveAttack();
            return;
        }
        // float distance = Vector3.Distance(transform.position, target.position);
        // if (distance < chaseDistance && !isDiving && !isCoolingDown)
        // {
        //     ChasePlayer();
        // }
        // else
        // {
        //     Patrol();
        //     chaseTimer = 0;
        // }
        if (isPatrolling)
        {
            Patrol();
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolDuration)
            {
                isPatrolling = false;
                patrolTimer = 0f;
                ChasePlayer();
            }
        }
        else
        {
            ChasePlayer();
        }
        
    }

    void InitializeWaypoints()
    {
        // Fill the waypoints queue with initial random positions
        for (int i = 0; i < maxWaypoints; i++)
        {
            waypoints.Enqueue(GenerateRandomPosition());
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float xPos = Random.Range(xMin, xMax);
        float zPos = Random.Range(zMin, zMax);
        return new Vector3(xPos, transform.position.y, zPos);
    }

    void SetNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            currentWaypoint = waypoints.Dequeue();

            // Generate a new waypoint if we're below the limit
            if (waypoints.Count < maxWaypoints)
            {
                waypoints.Enqueue(GenerateRandomPosition());
            }
        }
        else
        {
            // If no waypoints are left, generate one immediately
            currentWaypoint = GenerateRandomPosition();
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, currentWaypoint) < 1f)
        {
            SetNextWaypoint();
        }
        else
        {
            Vector3 direction = (currentWaypoint- transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(transform.position + direction);
        }
    }

    void ChasePlayer()
    {
        Vector3 flatTargetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (flatTargetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(flatTargetPosition);
        chaseTimer += Time.deltaTime;

        if (chaseTimer >= chaseTime)
        {
            isDiving = true;
            diveTimer = 0;
            chaseTimer = 0;
        }
    }

    void DiveAttack()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * diveSpeed * Time.deltaTime;
        transform.LookAt(target.position);

        diveTimer += Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 1f || diveTimer > diveDuration)
        {
            // Call DecreaseHealth on the player before ending the dive
            DecreaseHealth(target);
            EndDive();
        }
    }
    void DecreaseHealth(Transform player)
    {
          PlayerState.Instance.DecreaseHealth(); // Call the method that decreases the player's health
        
    }
    void EndDive()
    {
        isDiving = false;
        isCoolingDown = true;
        SetNextWaypoint();
    }
}
