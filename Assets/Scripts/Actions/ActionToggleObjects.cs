using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Toggle Objects")]
public class ActionToggleObjects : ActionShowHideObjects
{
    protected override bool GetFinalState(GameObject obj)
    {
        return !obj.activeSelf;
    }
}
