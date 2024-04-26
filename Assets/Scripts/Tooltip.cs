using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField] private string _tooltip;
    [SerializeField] private bool   _asSpeech;
    [SerializeField] private float  _offsetY = 0.2f;
    [SerializeField] private Color  _bgColor = Color.white;
    [SerializeField] private Color  _fgColor = Color.black;

    public string tooltip => _tooltip;
    public bool tooltipIsSpeech => _asSpeech;
    public float offsetY => _offsetY;
    public Color bgColor => _bgColor;
    public Color fgColor => _fgColor;

    protected override bool OnRun()
    {
        return true;
    }
}
