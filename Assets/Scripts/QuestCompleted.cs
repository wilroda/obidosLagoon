using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleted : MonoBehaviour
{
    [SerializeField] private Quest _quest;

    public Quest quest => _quest;

    // Update is called once per frame
    public void Execute()
    {
        // Check if object has actions
        var actions = GetComponents<Action>();
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
    }
}
