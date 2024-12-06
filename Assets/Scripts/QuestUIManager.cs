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

            questText.text = "Second Quest:\nTalk to Jimmy at PGH";
        }
        else if (!QuestManager.Instance.IsThirdQuestComplete)
        {
            questText.text = "You look injured!" +
                    "\nCome to my dorm to heal!";
        }
        else
        {
            questText.text = "Head to the library to find the professor.\nHe may have the antitode.";

        }
    }
}

}
