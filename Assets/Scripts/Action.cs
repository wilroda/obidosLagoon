using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    [HorizontalLine(color: EColor.Blue)]
    public bool canRetrigger = false;

    [HorizontalLine(color: EColor.Red)]
    [SerializeField] private Token[] requiredTokens;
    [SerializeField] private Token[] forbiddenTokens;

    private bool CheckConditions()
    {
        if (requiredTokens != null)
        {
            foreach (var t in requiredTokens)
            {
                if (!InventoryManager.HasToken(t)) return false;
            }
        }
        if (forbiddenTokens != null)
        {
            foreach (var t in forbiddenTokens)
            {
                if (InventoryManager.HasToken(t)) return false;
            }
        }

        if (!CustomConditions()) return false;

        return true;
    }

    protected virtual bool CustomConditions()
    {
        return true;
    }

    public void Run()
    {
        if (!CheckConditions())
        {
            return;
        }

        if (OnRun())
        {
            if (!canRetrigger)
            {
                enabled = false;
            }
        }
    }

    protected abstract bool OnRun();
}