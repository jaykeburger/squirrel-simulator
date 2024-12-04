using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiedScript : MonoBehaviour
{
    [SerializeField] public GameObject DiedMenu;

    // Update is called once per frame
    void Update()
    {
        if (GlobalValues.currentHealth <= 0)
        {
            DiedMenu.SetActive(true);

            // Stop time
            PauseScript.GameIsPause = true;
            Time.timeScale = 0f;

            // Unlock the cursor for menu navigation
            Cursor.lockState = CursorLockMode.None;

            // Stop animations if applicable
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            // Log the death event
            Debug.Log("You died");
        }
    }
    public void QuitGame()
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // If we can make save/load system
    public void Load()
    {
        Time.timeScale = 1f; 
        DiedMenu.SetActive(false);
        SceneManager.LoadScene("first-scene");
    }
}
