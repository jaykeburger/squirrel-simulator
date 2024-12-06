using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountBooks : MonoBehaviour
{
    public TextMeshProUGUI bookText;
    public static int bookCollected = 0;

    void Start()
    {
        bookText.text = "Book: " + GlobalValues.bookCount.ToString();
    }

    void Update()
    {
        bookText.text = "Book: " + GlobalValues.bookCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Book")
        {
            GlobalValues.bookCount += 1;
            bookCollected += 1;
            bookText.text = "Book: " + GlobalValues.bookCount.ToString();
            Destroy(other.gameObject);
        }
    }
}
