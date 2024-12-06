using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    // Add a public property to check the quest completion
    public bool IsFirstQuestComplete { get; private set; }
    public bool IsSecondQuestComplete { get; private set; }
    public int PaperCount { get; private set; }
    public int PencilCount { get; private set; }
    public bool IsThirdQuestComplete { get; private set; }
    public bool IsHealthRegenerated { get; set; }  // This flag can be set by another script that manages the player's health

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogError("Destroying duplicate instance of the QuestManager.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep quest manager across scenes
            Debug.Log("QuestManager instance created.");
        }
    }

    public void CollectItem(string itemType)
    {
        switch (itemType)
        {
            case "paper":
                PaperCount++;
                Debug.Log("Collected Paper: " + PaperCount);  // Log collection of paper
                break;
            case "pencil":
                PencilCount++;
                Debug.Log("Collected Pencil: " + PencilCount);  // Log collection of pencil
                break;
        }

        CheckQuestCompletion();
    }

    private void CheckQuestCompletion()
    {
        if (PaperCount >= 3 && PencilCount >= 1)
        {
            IsFirstQuestComplete = true;
            Debug.Log("Quest Completed!");
            // Implement what happens when the quest is completed
        }
    }
    public void CompleteSecondQuest()
    {
        IsSecondQuestComplete = true;
        Debug.Log("Second Quest Completed!");

    }
    public void CheckHealthRegeneration()
    {
            if (!IsThirdQuestComplete)
            {
            IsThirdQuestComplete = true;
            Debug.Log("Third Quest Completed! Health has been regenerated.");

            // Implement additional actions when the quest is completed, if necessary
            }
        
    }

    
}

