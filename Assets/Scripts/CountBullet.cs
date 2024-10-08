using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CountBullet : MonoBehaviour
{
    private int acorn_int = 0;
    private int rock_int = 0;
    public TextMeshProUGUI acornText;
    public TextMeshProUGUI rockText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Acorn")
        {
            acorn_int ++;
            acornText.text = "Acorn: " + acorn_int.ToString();
            Debug.Log(acorn_int);
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Rock")
        {
            rock_int ++;
            rockText.text = "Rock: " + rock_int.ToString();
            Debug.Log(acorn_int);
            Destroy(other.gameObject);
        }
    }
}
