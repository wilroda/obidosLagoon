using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Redirect")]
public class ActionRedirect : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private GameObject targetObject;

    protected override bool OnRun()
    {
        // Check if object has actions
        var actions = targetObject.GetComponents<Action>();
        if (actions.Length > 0)
        {
            foreach (var action in actions)
            {
                if (action.enabled)
                {
                    action.Run();
                }
            }
        }
        return true;
    }
}
