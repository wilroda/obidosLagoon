using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Show Objects")]
public class ActionShowObjects : ActionShowHideObjects
{
    protected override bool GetFinalState(GameObject obj)
    {
        return true;
    }
}
