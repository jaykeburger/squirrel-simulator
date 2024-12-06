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

    // [SerializeField]
    // private AudioClip DamageClip;

    // [SerializeField]
    // private AudioClip DeathClip;

    // private AudioSource DamageSound; // Sound for taking damage
    // private AudioSource DeathSound; // Sound for death

    AudioManager audioManager;

    // Start is called before the first frame update
    private void Start()
    {
        currenHealth = maxHealth;
        healthBar.value = currenHealth;
        enemyController = FindAnyObjectByType<GenerateEnemies>(); // Get the object that holds the GenerateEnemies script.

        // DamageSound = gameObject.AddComponent<AudioSource>();
        // DeathSound = gameObject.AddComponent<AudioSource>();

        // DamageSound.clip = DamageClip;
        // DeathSound.clip = DeathClip;
        // DamageSound.playOnAwake = false;
        // DeathSound.playOnAwake = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currenHealth; // Update slider
    }

    public void takeDamge(int amount)
    {
        Debug.Log("Taking Damage");
        // if (DamageSound != null && DamageSound.clip != null)
        // {
        //     DamageSound.Play(); // Play damage sound
        // }    

        audioManager.PlaySFX(audioManager.damage); // Play damage sound

        currenHealth -= amount;
     
        if (currenHealth <= 0)
        {
            if (gameObject.name == "dr-evyll")
            {
                FinalBossScript.isDead = true;
            }
            else
            {
                enemyController.removeEnemyPosition(gameObject);
                gameObject.SetActive(false); // Deactivate enemy immediately
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                currenHealth = maxHealth;
            }
            // PlayDeathSound(); // Call method to handle death sound
            audioManager.PlaySFX(audioManager.Death); // Play Death sound
            
        }
    }

    // private void PlayDeathSound()
    // {
    //     // Create a temporary GameObject to play the death sound
    //     GameObject deathSoundObject = new GameObject("DeathSound");
    //     AudioSource deathAudioSource = deathSoundObject.AddComponent<AudioSource>();

    //     // Configure the audio source
    //     deathAudioSource.clip = DeathClip;
    //     deathAudioSource.playOnAwake = false;
    //     deathAudioSource.Play();

    //     // Destroy the temporary GameObject after the sound finishes playing
    //     Destroy(deathSoundObject, DeathClip.length);
    // }
}
