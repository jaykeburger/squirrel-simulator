using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private GenerateEnemies enemyController;
    public Slider healthBar;
    public float maxHealth = 100f;
    public float currenHealth;
    public ParticleSystem deathEffect;


    // Start is called before the first frame update
    private void Start()
    {
        currenHealth = maxHealth;
        healthBar.value = currenHealth;
        enemyController = FindAnyObjectByType<GenerateEnemies>(); // Get the object that holds the GenerateEnemies script.
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currenHealth; // Update slider
    }

    public void takeDamge()
    {
        currenHealth -= 20;
        if (currenHealth <= 0)
        {
            Vector3 enemyPos = transform.position;
            enemyController.removeEnemyPosition(gameObject);
            gameObject.SetActive(false); // Deactivate enemy when current health reaches 0
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            currenHealth = maxHealth;
        }
    }
}
