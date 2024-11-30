using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Squirrel : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Hard code the transform scale for the squirrel
        if (SceneManager.GetActiveScene().name == "first-scene")
        {
            transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
        }
        else if (SceneManager.GetActiveScene().name == "JimmyDorm")
        {
            transform.localScale = new Vector3(0.44f, 0.44f, 0.44f);
        } else if (SceneManager.GetActiveScene().name == "PGH232")
        {
            transform.localScale = new Vector3(0.37f, 0.37f, 0.37f);
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0 || h < 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
}
