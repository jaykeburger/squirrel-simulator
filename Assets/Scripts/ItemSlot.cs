using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script is attached for each slot in the inventory.
/// Update the slot when item is dropped at an available slot.
/// </summary>
public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int slotID;
    public GameObject item
    {
        get
        {
            // There is already an item in this slot
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedItem = DragDrop.itemBeingDragged;
        GameObject prevSlot = DragDrop.startParent.gameObject;

        //  If the slot is available to store item
        if (!item)
        {
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.localPosition = new Vector2(0,0); //Make the item stays at the center of the slot

            // Update the idx in the GlobalInventory system
            InventorySystem.instance.UpdateGlobalInventory(draggedItem, this.gameObject, prevSlot);
        }
    }
}
