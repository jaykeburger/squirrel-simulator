using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public static float health = 100f;
    public float currentHealth = health;

    void Update()
    {
        currentHealth = health;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected from: " + other.gameObject.name);
        if (other.CompareTag("Bullet"))
        {
            health -= 20f;
            health = Mathf.Max(0, health);
        }
    }
}
