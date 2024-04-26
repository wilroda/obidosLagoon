using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Show Objects")]
public class ActionShowObjects : Action
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
                obj.SetActive(true);
            }
        }

        return true;
    }
}
