using UnityEngine;

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
            }
        }
    }
}
