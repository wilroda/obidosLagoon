using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Remove Token")]
public class ActionRemoveToken : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Token token;
    [SerializeField]
    private Token[] extraTokens;

    protected override bool OnRun()
    {
        InventoryManager.RemoveToken(token);
        if (extraTokens != null)
        {
            foreach (var t in extraTokens)
            {
                InventoryManager.RemoveToken(t);
            }
        }

        return true;
    }
}
