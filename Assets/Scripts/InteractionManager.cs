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
                // Check if object is interactable
                Debug.Log($"Found {h.collider.name}");
            }
        }
    }
}
