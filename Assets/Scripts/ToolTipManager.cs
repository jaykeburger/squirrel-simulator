using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager instance;
    public TextMeshProUGUI text;
    private Canvas canvas;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true; // Allow custom sorting order
        canvas.sortingOrder = 0;       // Default low sorting order
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(-30f, -30f, 0f);
    }

    public void SetAndShowToolTip(string message)
    {
        gameObject.SetActive(true);
        canvas.sortingOrder = 10;
        text.text = message + "\nDouble Click to Consume";
    }

    public void HideToolTip()
    {
        canvas.sortingOrder = 0;
        gameObject.SetActive(false);
        text.text = string.Empty;
    }
}
