using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    [InfoBox("Here we can activate conditions", EInfoBoxType.Normal)]
    public bool hasConditions = false;

    public abstract void Run();
}
