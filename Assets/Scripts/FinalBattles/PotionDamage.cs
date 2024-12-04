using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDamage : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            //GlobalValues.currentHealth -= 10;
            PlayerState.Instance.DecreaseHealth();
        }
    }
}
