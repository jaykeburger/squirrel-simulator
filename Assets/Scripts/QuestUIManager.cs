using UnityEngine;
using TMPro;  // Ensure you are using TextMeshPro

public class QuestUIManager : MonoBehaviour
{
    public TextMeshProUGUI questText;  // Reference to the TextMeshProUGUI component

   void Update()
{
    if (QuestManager.Instance != null)
    {
        if (!QuestManager.Instance.IsFirstQuestComplete)
        {
            // Text for the first quest
            questText.text = "Collect\n" +
                             (3 - QuestManager.Instance.PaperCount) + " Papers\n" +
                             (1 - QuestManager.Instance.PencilCount) + " Pencil";
        }
        else if (!QuestManager.Instance.IsSecondQuestComplete)
        {
            // Text for the second quest once the first is complete
            questText.text = "Second Quest:\nGet close to Jimmy.";
        }
        else
        {
            // Optional: Message when all quests are complete
            questText.text = "All quests completed!";
        }
    }
}

}
