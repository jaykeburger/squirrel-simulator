using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 50.0f;
    private float timeToDestroy = 3.0f;
    private int damage;
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    public void SetDamage(int weaponDamage)
    {
        damage = weaponDamage;
    }

    void Update()
    {
        // The move bullet to the position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < 0.01f) //In the case we hit OR miss the target.
        {
            Destroy(gameObject); //A.k.a destroy the bullet (make it disappear)
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Transform enemyTransform = other.collider.transform.parent;

            // Get the EnemyHealthBar component from the parent
            EnemyHealthBar enemyHealthBar = enemyTransform.GetComponent<EnemyHealthBar>();
            if (enemyHealthBar != null)
            {
                enemyHealthBar.takeDamge(); // Call the takeDamage function
            }
            else
            {
                Debug.LogError("EnemyHealthBar component not found on enemy-rat!");
            }
            Destroy(gameObject);
        }
    }

}
