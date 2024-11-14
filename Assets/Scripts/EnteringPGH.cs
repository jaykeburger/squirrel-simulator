using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteringPGH : MonoBehaviour
{
    [SerializeField] private GameObject asyncManager;
    public void OnTriggerEnter(Collider other)
    {
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
}
