using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDamage : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalValues.currentHealth -= 10;
        }
    }
}
