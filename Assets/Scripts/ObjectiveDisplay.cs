using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Set(Color color, string text)
    {
        if (this.text == null)
        {
            this.text = GetComponent<TextMeshProUGUI>();
        }

        this.text.text = text;
        this.text.color = color;
    }
}
