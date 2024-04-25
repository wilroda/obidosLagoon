using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayers;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            var hits = Physics.RaycastAll(ray, 1000.0f, ~ignoreLayers, QueryTriggerInteraction.Collide);
            Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

            foreach (var h in hits)
            {
                // Check if object has actions
                var actions = h.collider.GetComponents<Action>();
                if (actions.Length > 0)
                {
                    foreach (var action in actions)
                    {
                        action.Run();
                    }
                }
                else
                {
                    // No actions, might want to do something here (play a sound or something)
                }

                // Don't check anymore objects (they're hidden behind this one)
                break;
            }
        }
    }
}
