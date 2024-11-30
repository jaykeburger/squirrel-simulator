using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This script is attached to the item that is in the inventory.
/// Add +5 (if bread) and -5 (if mushroom) health to player when double click the item
/// </summary>
public class InventoryItems : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Consumption parameters
    private GameObject itemPendingForConsumption;
    private const float doubleClickThreshold = 0.2f;
    private float lastClickTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            float timeSinceLastClick = Time.unscaledTime - lastClickTime;
            lastClickTime = Time.unscaledTime;
            if (timeSinceLastClick < doubleClickThreshold)
            {
                if (gameObject.name.Contains("Mushroom"))
                {
                    GlobalValues.currentHealth -= 5f;
                    itemPendingForConsumption = gameObject;
                }
                else if (GlobalValues.currentHealth < GlobalValues.maxHealth)
                {
                    GlobalValues.currentHealth += 5f;

                    // Make sure that we choose the lasted object that we click to destroy later,
                    // if not, the function will destroy every object in the inventory that we click on.
                    itemPendingForConsumption = gameObject; 
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemPendingForConsumption == gameObject)
            {
                // Delete the item from the Global inventory memory
                GameObject parentSlot = transform.parent.gameObject;
                if (parentSlot == null)
                {
                    Debug.Log("parent object is null");
                }
                else
                {
                    int idx = parentSlot.GetComponent<ItemSlot>().slotID;
                    Debug.Log("Slot index when delete: " + idx);

                    GlobalValues.GlobalInventory.Remove(idx);
                }

                DestroyImmediate(gameObject);

                // Hide ToolTip when consume item
                ToolTipManager.instance.HideToolTip();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string message;
        int idx = gameObject.name.IndexOf('(');
        if (idx != -1)
        {
            message = gameObject.name.Substring(0, idx);
        }
        else
        {
            message = gameObject.name;
        }
        ToolTipManager.instance.SetAndShowToolTip(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.instance.HideToolTip();
    }
}
