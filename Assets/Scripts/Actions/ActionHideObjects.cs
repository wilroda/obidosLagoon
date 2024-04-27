using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Hide Objects")]
public class ActionHideObjects : ActionShowHideObjects
{
    protected override bool GetFinalState(GameObject obj)
    {
        return false;
    }
}
