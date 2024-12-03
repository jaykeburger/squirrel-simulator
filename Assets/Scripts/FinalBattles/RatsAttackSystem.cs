using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class RatsAttackSystem : MonoBehaviour
{
    public static bool first;
    public GameObject firstGroup;
    public GameObject secondGroup;
    public List<LightsUpPaths> paths = new List<LightsUpPaths>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in firstGroup.transform)
        {
            if (child.CompareTag("RatGroup"))
            {
                child.gameObject.SetActive(false);
            }
        }
        foreach(Transform child in secondGroup.transform)
        {
            if (child.CompareTag("RatGroup"))
            {
                child.gameObject.SetActive(false);
            }
        }
        LightsUpPaths[] pathArray = GetComponentsInChildren<LightsUpPaths>();
        foreach (LightsUpPaths path in pathArray)
        {
            paths.Add(path); // Add each child to the list
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FinalBossScript.activeRats)
        {
            if (!first)
            {
                foreach(Transform child in secondGroup.transform)
                {
                    if (child.CompareTag("RatGroup"))
                    {
                        child.gameObject.SetActive(true);
                        ActiveGroup(secondGroup);
                    }
                }
                foreach(Transform child in firstGroup.transform)
                {
                    if (child.CompareTag("RatGroup"))
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach(Transform child in firstGroup.transform)
                {
                    if (child.CompareTag("RatGroup"))
                    {
                        child.gameObject.SetActive(true);
                        ActiveGroup(firstGroup);
                    }
                }
                foreach(Transform child in secondGroup.transform)
                {
                    if (child.CompareTag("RatGroup"))
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void ActiveGroup(GameObject group)
    {
        // Call showPath() on all LightsUpPaths if the group becomes active
        foreach (LightsUpPaths path in paths)
        {
            if (path.gameObject.activeSelf)
            {
                path.showPath();
            }
        }
    }
}
