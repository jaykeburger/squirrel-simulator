using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLi : MonoBehaviour
{
    [SerializeField] private GameObject asyncManager;
    public GameObject dialogCanvas;

    public void OnTriggerEnter(Collider other)
    {
        if (QuestManager.Instance != null && QuestManager.Instance.IsThirdQuestComplete)
        {
            AsyncLoader sceneLoader = asyncManager.GetComponent<AsyncLoader>();
            if (sceneLoader != null)
            {
                sceneLoader.LoadScene("LibraryInside");
            }
        }
        else
        {
            Debug.Log("Quest not completed yet. Cannot enter Library.");
            dialogCanvas.SetActive(true);
        }
    }
}
