using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSCript : MonoBehaviour
{
    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        MakeTransparent();
    }

    void MakeTransparent()
    {
        // Ensure the object has a Renderer component
        if (objRenderer != null)
        {
            // Set the material's shader to transparent (if not already)
            objRenderer.material.SetFloat("_Mode", 3); // 3 = Transparent mode for Unity Standard Shader

            // Set the material's transparency by modifying the color's alpha
            Color color = objRenderer.material.color;
            color.a = 0f; // 0 is fully transparent
            objRenderer.material.color = color;

            // Optionally, disable shadow casting (so it doesn't cast a shadow when invisible)
            objRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }
}
