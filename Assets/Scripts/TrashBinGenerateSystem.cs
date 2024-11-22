using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the TrashBinSystem Game Object.
/// Generates a list of trash bin and assigns ID for each.
/// Randomly choose a trash bin to contrain collectable object.
/// </summary>
public class TrashBinGenerateSystem : MonoBehaviour
{
    public GameObject trashBinGroup;
    public List<GameObject> trashBinList = new();
    public static TrashBinGenerateSystem Instance { get;private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        populateTrashBinsList();
    }

    // Update is called once per frame
    void Update()
    {
        // If there is no trash bin is chosen to store an object;
        if (GlobalValues.binIsChose == false)
        {
            int chosenBinIdx = Random.Range(0, 6); //Randomly chose a bin to store an collectable object
            GlobalValues.binIsChose = true;
            GlobalValues.binIsChoseID = chosenBinIdx;
            Debug.Log("Random int: " + chosenBinIdx);
        }
    }

    public void populateTrashBinsList()
    {
        int idx = 0;
        foreach(Transform child in trashBinGroup.transform)
        {
            if (child.CompareTag("TrashBin"))
            {
                trashBinList.Add(child.gameObject);
                child.gameObject.AddComponent<InteractableObject>().binID = idx; //Tag index for each trash bin child
                idx ++;
            }
        }
    }
}
