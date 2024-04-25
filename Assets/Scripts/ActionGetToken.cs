using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Get Token")]
public class ActionGetToken : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Token token;
    [SerializeField]
    private Token[] extraTokens;

    protected override bool OnRun()
    {
        InventoryManager.AddToken(token);
        if (extraTokens != null)
        {
            foreach (var t in extraTokens)
            {
                InventoryManager.AddToken(t);
            }
        }

        return true;
    }
}
