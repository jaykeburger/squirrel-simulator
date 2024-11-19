using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiedScript : MonoBehaviour
{
    [SerializeField] public GameObject DiedMenu;

    // Update is called once per frame
    void Update()
    {
        if (GlobalValues.currentHealth == 0)
        {
            DiedMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void QuitGame()
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // If we can make save/load system
    public void Load()
    {

    }
}
