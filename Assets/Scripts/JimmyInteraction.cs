using UnityEngine;
using UnityEngine.SceneManagement;

public class JimmyInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (QuestManager.Instance != null && !QuestManager.Instance.IsSecondQuestComplete)
            {
                Debug.Log("Sending that Second Quest Completed!");
                QuestManager.Instance.CompleteSecondQuest();
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Comic-3.5");
            }
        }
    }
}
