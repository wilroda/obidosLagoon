using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Hide Objects")]
public class ActionHideObjects : Action
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
                obj.SetActive(false);
            }
        }

        return true;
    }
}
