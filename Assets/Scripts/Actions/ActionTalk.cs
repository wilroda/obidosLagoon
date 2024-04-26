using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Talk")]
public class ActionTalk : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Transform who;
    [SerializeField, ShowIf("hasTransform")]
    private float offsetY = 0.1f;
    [SerializeField, TextArea]
    private string text;
    [SerializeField]
    private Color bgColor = Color.white;
    [SerializeField]
    private Color fgColor = Color.black;
    [SerializeField]
    private float duration = 2.0f;

    bool hasTransform => who != null;

    protected override bool OnRun()
    {
        SpeechManager.Say(who, text, bgColor, fgColor, duration, offsetY);

        return true;
    }
}
