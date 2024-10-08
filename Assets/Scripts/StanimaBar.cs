using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanimaBar : MonoBehaviour
{
    private Slider slider;
    public Text staminaCounter;
    public GameObject playerState;
    private float currentStamina, maxStamina;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentStamina = playerState.GetComponent<PlayerState>().currentStamina;
        maxStamina = playerState.GetComponent<PlayerState>().maxStamina;
        
        float fillValue = currentStamina / maxStamina;
        slider.value = fillValue; //Fill in the slider value
        staminaCounter.text = currentStamina + "/" + maxStamina;
    }
}
