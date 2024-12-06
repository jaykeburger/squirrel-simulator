using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    //----- Player Health ------//
    private bool isHealing = false;
    public float healthDamage = 10.0f;

    //----- Player Stamina -----//
    public float staminaRecoveryRate = 5.0f;

    //----- Damage Sound -----//
    [SerializeField]
    private AudioClip damageClip;  // Drag your damage sound clip here in the Inspector
    private AudioSource damageSound; // Audio source for playing damage sound

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
        GlobalValues.currentHealth = Mathf.Min(GlobalValues.currentHealth, GlobalValues.maxHealth);
        GlobalValues.currentStamina = Mathf.Min(GlobalValues.currentStamina, GlobalValues.maxStamina);
        // currentHealth = maxHealth;
        // currentStamina = maxStamina;

         // Initialize and configure the damage sound
        damageSound = gameObject.AddComponent<AudioSource>();
        damageSound.clip = damageClip;
        damageSound.playOnAwake = false;
    }

    void Update()
    {
        if (GlobalValues.currentHealth == 0)
        {
            // GlobalValues.currentHealth = GlobalValues.maxHealth;
        }
        
        if (GlobalValues.currentHealth < GlobalValues.maxHealth)
        {
            if (!isHealing)
            {
                StartCoroutine(RecoverHealth());
            }
        }
    }

    public void DecreaseHealth()
    {
        // Play the damage sound when health is decreased
        if (damageSound != null && damageSound.clip != null)
        {
            damageSound.Play();
        }
        GlobalValues.currentHealth -= healthDamage;
        // GlobalValues.currentHealth = Mathf.Max(0, GlobalValues.currentHealth);
    }

    IEnumerator RecoverHealth()
    {
        if (SceneManager.GetActiveScene().name == "JimmyDorm")
        {
            isHealing = true;

            while (GlobalValues.currentHealth < GlobalValues.maxHealth){
                yield return new WaitForSeconds(3f); //Wait for 3s each time we increase health
                GlobalValues.currentHealth += 10;
                GlobalValues.currentHealth = Mathf.Min(GlobalValues.currentHealth, GlobalValues.maxHealth); //Ensure that current health won't exceed maxHealth
            }
            QuestManager.Instance.CheckHealthRegeneration();
            isHealing = false;
        }
    }

    // Function to reduce stamina if player is sprinting
    public void UseStamina(float deltaTime, float StaminaUsedRate)
    {
        GlobalValues.currentStamina -= StaminaUsedRate * deltaTime;
        if (GlobalValues.currentStamina < 0)
        {
            GlobalValues.currentStamina = 0;
        }
    }

    //Function to recover stamina if player is not sprinting
    public void RecoverStamina(float deltaTime)
    {
        GlobalValues.currentStamina += staminaRecoveryRate * deltaTime;
        if (GlobalValues.currentStamina > GlobalValues.maxStamina)
        {
            GlobalValues.currentStamina = GlobalValues.maxStamina;
        }
    }
}