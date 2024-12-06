using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public static bool isTrigger = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform trigger = transform.Find("BossFightTrigger");
            if (trigger != null)
            {
                trigger.gameObject.GetComponent<Collider>().isTrigger = false;
                FinalBossScript.isBattleActive = true;
                Debug.Log("From trigger door " + FinalBossScript.isBattleActive);
            }
            else
            {
                Debug.Log("Trigger is not found");
            }
        }
    }
}
