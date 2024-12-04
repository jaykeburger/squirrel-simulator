using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcronGroupScript : MonoBehaviour
{
public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GlobalValues.currentHealth -= 5;
        }

        if (other.CompareTag("Plane"))
        {
            Destroy(gameObject);
        }
    }
}
