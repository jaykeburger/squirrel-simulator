using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is attached to the bread prefab.
/// Trigger the AddToInventory from InvetorySystem script.
/// </summary>
public class AddBreadToInventory : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        Debug.Log("Collected Bread");
        InventorySystem.instance.AddToInventory("Bread");
    }
}
