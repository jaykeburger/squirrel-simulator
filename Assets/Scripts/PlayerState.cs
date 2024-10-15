using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    //----- Player Health ------//
    public float currentHealth;
    public float maxHealth;
    private bool isHealing = false;

    //----- Player Stamina -----//
    public float currentStamina;
    public float maxStamina;
    // public float staminaUseRate = 10.0f;
    public float staminaRecoveryRate = 5.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    void Update()
    {
        // Simulate taking damage with K key
        if (Input.GetKeyDown(KeyCode.K))
        {
            DecreaseHealth(10);
        }   
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
    }

    void DecreaseHealth(int amount)
    {
        currentHealth -= amount;

        if (!isHealing)
        {
            StartCoroutine(RecoverHealth());
        }
    }

    IEnumerator RecoverHealth()
    {
        if (SceneManager.GetActiveScene().name == "JimmyDorm")
        {
            isHealing = true;

            while (currentHealth < maxHealth){
                yield return new WaitForSeconds(3f); //Wait for 3s each time we increase health
                currentHealth += 10;
                currentHealth = Mathf.Min(currentHealth, maxHealth); //Ensure that current health won't exceed maxHealth
            }
            isHealing = false;
        }
    }

    // Function to reduce stamina if player is sprinting
    public void UseStamina(float deltaTime, float StaminaUsedRate)
    {
        currentStamina -= StaminaUsedRate * deltaTime;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    //Function to recover stamina if player is not sprinting
    public void RecoverStamina(float deltaTime)
    {
        currentStamina += staminaRecoveryRate * deltaTime;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }
}