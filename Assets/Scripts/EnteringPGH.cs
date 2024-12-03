using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteringPGH : MonoBehaviour
{
    [SerializeField] 
    private GameObject asyncManager;
    public GameObject dialogCanvas;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the collider is the player
        {
            // Check if the first quest is completed
            if (QuestManager.Instance != null && QuestManager.Instance.IsFirstQuestComplete)
            {
                // Store the player's position
                PlayerPosition.lastPlayerPosition = other.transform.position;

                // Load the new scene
                AsyncLoader sceneLoader = asyncManager.GetComponent<AsyncLoader>();
                if (sceneLoader != null)
                {
                    sceneLoader.LoadScene("PGH232");
                }
                else
                {
                    Debug.LogError("sceneLoader not found on this GameObject.");
                }
            }
            else
            {
                Debug.Log("Quest not completed yet. Cannot enter PGH232.");
                dialogCanvas.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogCanvas.SetActive(false);   
        }
    }
}
