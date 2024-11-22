using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script is triggered when player eat or touches the poison mushroom.
/// Call the coroutine of the wobble effect from the WobbleEffect script.
/// </summary>
public class wobbleEffectManager : MonoBehaviour
{
    public WobbleEffect _wobbleEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WobbleOn();
        }
    }

    public void WobbleOn()
    {
        Debug.Log("Wobble on");
        _wobbleEffect.enabled = true;
        _wobbleEffect.StartWobble();
    }
}
