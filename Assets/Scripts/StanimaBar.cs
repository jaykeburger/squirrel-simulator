using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanimaBar : MonoBehaviour
{
    private Slider slider;
    public Text staminaCounter;
    private float currentStamina, maxStamina;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        currentStamina = GlobalValues.currentStamina;
        maxStamina = GlobalValues.maxStamina;
        
        float fillValue = currentStamina / maxStamina;
        slider.value = fillValue; //Fill in the slider value
        staminaCounter.text = currentStamina + "/" + maxStamina;
    }
}
