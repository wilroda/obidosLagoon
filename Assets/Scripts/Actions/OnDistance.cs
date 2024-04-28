using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/_Conditions/On Distance")]
public class OnDistance : MonoBehaviour
{
    [SerializeField] private bool      canRetrigger;
    [SerializeField] private Transform objectToCheck;
    [SerializeField] private float     distance;

    private void Update()
    {
        if (!Level.isActive) return;

        if (Vector3.Distance(transform.position, objectToCheck.position) <= distance)
        {
            Execute();

            if (!canRetrigger)
            {
                enabled = false;
            }
        }
    }

    public void Execute()
    {
        // Check if object has actions
        var actions = GetComponents<Action>();
        if (actions.Length > 0)
        {
            foreach (var action in actions)
            {
                if (action.canRun)
                {
                    action.Run();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
