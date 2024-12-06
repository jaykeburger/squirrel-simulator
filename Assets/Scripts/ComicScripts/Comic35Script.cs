using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comic35Script : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public int count = 0;

    void Start()
    {
        // Panel1.gameObject.SetActive(false);
        Panel2.gameObject.SetActive(false);
        Panel3.gameObject.SetActive(false);
    }
    public void onclick()
    {
        count++;
        if (count == 1)
        {
            Panel2.gameObject.SetActive(true);
        }
        else if (count == 2)
        {
            Panel3.gameObject.SetActive(true);
        }
        else if (count == 3)
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("PGH232");
        }
    }
}
