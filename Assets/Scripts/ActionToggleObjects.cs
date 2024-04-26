using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Toggle Objects")]
public class ActionToggleObjects : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private GameObject[] objects;

    protected override bool OnRun()
    {
        if (objects != null)
        {
            foreach (var obj in objects)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }

        return true;
    }
}
