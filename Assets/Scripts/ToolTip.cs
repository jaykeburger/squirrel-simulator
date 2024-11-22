using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        Debug.Log("Hover");
        ToolTipManager.instance.SetAndShowToolTip(message);
    }

    private void OnMouseExit()
    {
        ToolTipManager.instance.HideToolTip();
    }
}
