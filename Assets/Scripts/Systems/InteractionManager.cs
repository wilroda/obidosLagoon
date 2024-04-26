using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask  ignoreLayers;
    [SerializeField] private Camera     mainCamera;

    private SpeechBubble    speechBubble;
    private GameOverUI      gameOver;

    void Start()
    {
        gameOver = FindObjectOfType<GameOverUI>(true);
    }

    // Update is called once per frame
    void Update()
    {
        bool clearTooltip = true;

        if (!gameOver.isGameOver)
        {
            // Cast a ray from the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            var hits = Physics.RaycastAll(ray, 1000.0f, ~ignoreLayers, QueryTriggerInteraction.Collide);
            if ((hits != null) && (hits.Length > 0))
            {
                Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

                foreach (var h in hits)
                {
                    var interactable = h.collider.GetComponent<ShowTooltip>();
                    if ((interactable != null) && (interactable.CheckConditions()))
                    {
                        string text = interactable.tooltip;
                        if ((text != "") && (text != null))
                        {
                            clearTooltip = false;

                            if (interactable.tooltipIsSpeech)
                            {
                                if (speechBubble == null)
                                {
                                    speechBubble = SpeechManager.Say(h.transform, text, interactable.bgColor, interactable.fgColor, float.MaxValue, interactable.offsetY, false);
                                }
                                else
                                {
                                    speechBubble.Set(h.transform, interactable.offsetY, mainCamera);
                                    speechBubble.Set(text, interactable.bgColor, interactable.fgColor);
                                }
                            }
                        }
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        // Check if object has actions
                        var actions = h.collider.GetComponents<Action>();
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
                        else
                        {
                            // No actions, might want to do something here (play a sound or something)
                        }
                    }

                    // Don't check anymore objects (they're hidden behind this one)
                    break;
                }
            }
        }

        if (clearTooltip)
        {
            ClearTooltip();
        }
    }

    void ClearTooltip()
    {
        if (speechBubble != null)
        {
            SpeechManager.Clear(speechBubble);
            speechBubble = null;
        }
    }
}
