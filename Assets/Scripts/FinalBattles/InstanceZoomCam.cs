using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceZoomCam : MonoBehaviour
{
    public static InstanceZoomCam Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
