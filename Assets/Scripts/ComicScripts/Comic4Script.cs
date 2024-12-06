using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comic4Script : MonoBehaviour
{
    public GameObject Panel1;
    public int count = 0;

    void Start()
    {
        Time.timeScale = 1f;
        // Panel1.gameObject.SetActive(false);
    }
    public void onclick()
    {
        count++;
        if (count == 1)
        {
            SceneManager.LoadScene("Comic-5"); //Change this to whatever scene you want
        }
    }
}
