using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHalo : MonoBehaviour
{
    public GameObject[] targetObjects; // Array of objects with the halo effect

    private Behaviour[] halos; // Array to store Halo components

    void Start()
    {
        halos = new Behaviour[targetObjects.Length];

        // Disable the halo on all target objects at the start
        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (targetObjects[i] != null)
            {
                halos[i] = (Behaviour)targetObjects[i].GetComponent("Halo");
                if (halos[i] != null)
                {
                    halos[i].enabled = false; // Disable halo
                }
                else
                {
                    Debug.LogWarning("Halo not found on " + targetObjects[i].name);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestManager.Instance != null && QuestManager.Instance.IsFirstQuestComplete)
        {
            halos[0].enabled = true;
            halos[1].enabled = true;
        }
        if (QuestManager.Instance != null && QuestManager.Instance.IsSecondQuestComplete)
        {
            halos[2].enabled = true;
        }
        if(QuestManager.Instance != null && QuestManager.Instance.IsThirdQuestComplete)
        {
            halos[3].enabled = true;
        }
    }
}
