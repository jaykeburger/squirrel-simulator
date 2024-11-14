using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Debug.Log(draggedItem.ToString());
        //  If the slot is available to store item
        if (!item)
        {
            draggedItem.transform.SetParent(transform);
            draggedItem.transform.localPosition = new Vector2(0,0); //Make the item stays at the center of the slot

            // Update the idx in the GlobalInventory system
            // InventorySystem.instance.UpdateGlobalInventory(draggedItem, this.gameObject, previousSlot);
        }
    }
}
