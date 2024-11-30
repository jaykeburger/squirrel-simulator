using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMoodyTowers : MonoBehaviour
{
    [SerializeField] private GameObject asyncManager;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the collider is the player
        {
            // Store the player's position
            PlayerPosition.lastPlayerPosition = other.transform.position;

            // Load the new scene
            AsyncLoader sceneLoader = asyncManager.GetComponent<AsyncLoader>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadScene("JimmyDorm");
            }
            else
            {
                Debug.LogError("sceneLoader not found on this GameObject.");
            }
        }
    }
}
