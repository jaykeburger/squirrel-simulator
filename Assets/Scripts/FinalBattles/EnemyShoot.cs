using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // Bullet prefab (sphere)
    public Transform player;         // Reference to the player
    public float shootingForce = 10f; // The force with which the bullet is shot
    public float shootingCooldown = 2f; // Time delay between shots
    public float maxDistance = 50f;
    private float nextShootTime = 0f;
    public bool isShootingActive = false;

    void Update()
    {
        // Check if enough time has passed since the last shot
        if (isShootingActive)
        {
            if (Time.time >= nextShootTime)
            {
                // Shoot the bullet towards the player
                // ShootAtPlayer();

                // Update the time for the next shot
                nextShootTime = Time.time + shootingCooldown;
            }
        }
    }

    public void StartShootingPlayer()
    {
        isShootingActive = true;
        StartCoroutine(ShootForDuration(10f));
    }

    private IEnumerator ShootForDuration(float duration)
    {
        float elapsedTime = 0f;

        // Continue shooting for the specified duration
        while (elapsedTime < duration)
        {
            // Shoot at the player
            ShootAtPlayer();
            // Wait for the shooting cooldown before shooting again
            yield return new WaitForSeconds(shootingCooldown);
            elapsedTime += shootingCooldown;
        }

        // Stop shooting after the duration
        isShootingActive = false;
    }

    void ShootAtPlayer()
    {
        if (player != null)
        {
            // Calculate direction from enemy to player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Instantiate the bullet at the enemy's position
            GameObject bullet = Instantiate(bulletPrefab, transform.position + directionToPlayer * 1f, Quaternion.identity);

            // Add force to the bullet in the direction of the player
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Disable gravity if necessary to keep the bullet moving horizontally
                rb.useGravity = false;

                // Set the bullet's velocity directly for immediate movement
                rb.velocity = directionToPlayer * shootingForce;

                // Store the bullet's starting position to track its distance
                Bullet bulletScript = bullet.AddComponent<Bullet>();
                bulletScript.startPosition = bullet.transform.position;
                bulletScript.maxDistance = maxDistance;
                bulletScript.rb = rb;
            }
        }
    }
}

public class Bullet : MonoBehaviour
{
    public Vector3 startPosition;
    public float maxDistance;
    public Rigidbody rb;

    void Update()
    {
        // Check the distance from the start position every frame
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            // Destroy the bullet if it exceeds the max distance
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Print "Hit" to the console
            Debug.Log("Hit");

            // Destroy the bullet upon collision with the player
            Destroy(gameObject);
        }
    }
}
