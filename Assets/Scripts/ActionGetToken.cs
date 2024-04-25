using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Get Object")]
public class ActionGetObject : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Token token;

    protected override bool OnRun()
    {
        InventoryManager.AddToken(token);

        return true;
    }
}
