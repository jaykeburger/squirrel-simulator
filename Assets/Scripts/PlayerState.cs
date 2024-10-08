using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    //----- Player Health ------//
    public float currentHealth;
    public float maxHealth;

    //----- Player Stamina -----//
    public float currentStamina;
    public float maxStamina;
    public float staminaUseRate = 10.0f;
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
            currentHealth -= 10;
        }   
        if (currentHealth == 0)
        {
            currentHealth = maxHealth;
        }
    }

    // Function to reduce stamina if player is sprinting
    public void UseStamina(float deltaTime)
    {
        currentStamina -= staminaUseRate * deltaTime;
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