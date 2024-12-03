using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcronGroupScript : MonoBehaviour
{
public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            GlobalValues.currentHealth -= 5;
        }
    }
}
