using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
/// <summary>
/// This script is attached to each trash bin.
/// Check if the right trash bin is interacted to give object to the inventory.
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [SerializeField] public bool playerInRange;
    public GameObject InteractableUI;
    public GameObject dialogCanvas;
    public TextMeshProUGUI text;
    public int binID;

    public List<string> items = new List<string>{"Bread", "Mushroom"};

    public void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            checkForBin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractableUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractableUI.SetActive(false);
            dialogCanvas.SetActive(false);
        }
    }

    public void checkForBin()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogCanvas.activeSelf){
                dialogCanvas.SetActive(true);
            }
            else
            {
                dialogCanvas.SetActive(false);
            }
            
            if (binID == GlobalValues.binIsChoseID)
            {
                Debug.Log("Hit the right bin");
                int randomItem = Random.Range(0,items.Count); // Range of items list
                string randomItemName = items[randomItem];
                text.text = "Ohhh " + randomItemName;
                Debug.Log(randomItemName);
                InventorySystem.instance.AddToInventory(randomItemName);
                GlobalValues.binIsChose = false;
            }
            else
            {
                Debug.Log("This bin is not chosen");
                text.text = "There is nothing in here";
            }
        }
    }
}
