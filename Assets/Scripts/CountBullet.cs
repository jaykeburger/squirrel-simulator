using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CountBullet : MonoBehaviour
{
    public TextMeshProUGUI acornText;
    public TextMeshProUGUI rockText;

    void Start()
    {
        acornText.text = "Acorn: " + GlobalValues.acornCount.ToString();
        rockText.text = "Rock: " + GlobalValues.rockCount.ToString();
    }

    void Update()
    {
        acornText.text = "Acorn: " + GlobalValues.acornCount.ToString();
        rockText.text = "Rock: " + GlobalValues.rockCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Acorn")
        {
            GlobalValues.acornCount +=5;
            acornText.text = "Acorn: " + GlobalValues.acornCount.ToString();
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "Rock")
        {
            GlobalValues.rockCount +=3;
            rockText.text = "Rock: " + GlobalValues.rockCount.ToString();
            Destroy(other.gameObject);
        }
    }
}
