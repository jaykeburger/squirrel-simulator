using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script is attached to Inventory system GameObject.
/// Hide and show the inventory canvas using Tab button.
/// Adding object into the inventory.
/// Save and load GlobalInventory in GlobalValues
/// Update inventory slot when item is dragged to a new slot
/// </summary>
public class InventorySystem : MonoBehaviour
{
    // Inventory parameters
    public static InventorySystem instance { get;private set; }
    public GameObject inventoryScreenUI;
    private GameObject parentCanvas;
    public List<GameObject> slotList = new();
    public List<string> itemList = new();
    private GameObject itemToAdd;
    private GameObject slotToAdd;
    private bool isOpen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        parentCanvas = inventoryScreenUI.transform.parent.gameObject;
        PopulateSlotList();
        LoadInventory();
    }

    //================== Populate the slots in inventory and add in items =========================
    private void PopulateSlotList()
    {
        int idx = 0; // Create index id to tag for each slot

        // Check for each child in the Canvas Object and add all of them into the slotList.
        foreach(Transform child in inventoryScreenUI.transform)
        {
            if(child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
                child.gameObject.AddComponent<ItemSlot>().slotID = idx; // Assign a slot ID for this slot
                idx ++;
            }
        }
    }

    public void AddToInventory(string itemName)
    {
        slotToAdd = FindNextEmptySlot();
        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), 
                    slotToAdd.transform.position, 
                    slotToAdd.transform.rotation);

        itemToAdd.transform.SetParent(slotToAdd.transform);
        itemToAdd.transform.localScale = Vector3.one; // Make sure it always has scale of 1
        itemList.Add(itemName);

        int idx = slotToAdd.GetComponent<ItemSlot>().slotID;
        Debug.Log("Slot at add " + idx);
        GlobalValues.GlobalInventory[idx] = itemName;
    }

    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0) //if that slot is not occupied.
            {
                return slot;
            }
        }
        return null;
    }

    public void LoadInventory()
    {
        Debug.Log("Global inventory count: " + GlobalValues.GlobalInventory.Count);

        if (GlobalValues.GlobalInventory.Count == 0)
        {
            return;
        }
        foreach(var entry in GlobalValues.GlobalInventory)
        {
            // Debug.Log(entry.Key);
            int retrievedID = entry.Key;
            Debug.Log("ID when loading inventory: " + entry.Key);
            GameObject slot = slotList.Find(s => s.GetComponent<ItemSlot>().slotID == retrievedID);
            if (slot == null)
            {
                Debug.Log("Slot is null");
            }
            else
            {
                Debug.Log("Item name at loading slot: " + GlobalValues.GlobalInventory[entry.Key]);
                GameObject prefab = Resources.Load<GameObject>(entry.Value);
                if (prefab == null)
                {
                    Debug.Log("item is null");
                }
                else
                {
                    GameObject item = Instantiate(prefab, slot.transform.position, slot.transform.rotation);
                    item.transform.SetParent(slot.transform);
                    item.transform.localScale = Vector3.one;
                }
            }
        }
    }

    public void UpdateGlobalInventory(GameObject item, GameObject newSlot, GameObject prevSlot)
    {
        int prevIdx = prevSlot.GetComponent<ItemSlot>().slotID;
        Debug.Log("Previous slot id: " + prevIdx);
        string newItemName = GlobalValues.GlobalInventory[prevIdx];
        Debug.Log("Item's name at new slot: " + newItemName);
        GlobalValues.GlobalInventory.Remove(prevIdx);
        int newIdx = newSlot.GetComponent<ItemSlot>().slotID;
        GlobalValues.GlobalInventory[newIdx] = newItemName;
        Debug.Log("new slot id: " + newIdx);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !isOpen)
        {
            parentCanvas.SetActive(true);
            isOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            PauseScript.GameIsPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && isOpen)
        {
            parentCanvas.SetActive(false);
            isOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            PauseScript.GameIsPause = false;
        }
    }
}
