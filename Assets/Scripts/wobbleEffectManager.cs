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
    private Keyboard _keyboard;
    // Start is called before the first frame update
    void Start()
    {
        _keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (_keyboard.pKey.wasPressedThisFrame)
        {
            WobbleOn();
        }
        else if (_keyboard.lKey.wasPressedThisFrame)
        {
            WobbleOff();
        }
    }

    private void WobbleOn()
    {
        Debug.Log("Wobble on");
        _wobbleEffect.enabled = true;
        _wobbleEffect.StartWobble();
    }

    private void WobbleOff()
    {
        _wobbleEffect.StopWobble();
    }
}
