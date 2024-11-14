using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class InventoryItems : MonoBehaviour, IPointerClickHandler
{
    // Consumption parameters
    private GameObject itemPendingForConsumption;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.5f;

    // ======================== Functions for comsumming item ===========================
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clicking bread");
            if (Time.time - lastClickTime <= doubleClickThreshold)
            {
                Debug.Log("Enter clicking");
                itemPendingForConsumption = gameObject;
                if (GlobalValues.currentHealth < GlobalValues.maxHealth)
                {
                    Debug.Log("Healing");
                    GlobalValues.currentHealth += 5f;
                }
            }
            DestroyImmediate(gameObject);
            // InventorySystem.instance.ReCalculateList();
        }
        else
        {
            Debug.Log("Single click");
        }
    }

    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     if (eventData.button == PointerEventData.InputButton.Left)
    //     {
    //         // Make sure we know which item should be consummed first
    //         if (itemPendingForConsumption == gameObject)
    //         {
    //             DestroyImmediate(gameObject);
    //             // InventorySystem.instance.ReCalculateList();
    //         }
    //     }
    // }
}
