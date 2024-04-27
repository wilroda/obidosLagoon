using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public enum CursorType { Default = 0, Eye = 1, Axe = 2, Pickaxe = 3, Shovel = 4, Sword = 5, Zoom = 6, Target = 7, 
                             Talk = 8, Hammer = 9, 
                             SciFiPointer = 10, CartoonPointer = 11, TrianglePointer = 12, 
                             Walk = 13, Open = 14, Interact = 15, Grab = 16 }

    [SerializeField] private LayerMask      ignoreLayers;
    [SerializeField] private Camera         mainCamera;
    [SerializeField] private Texture2D[]    cursors;

    private SpeechBubble    speechBubble;
    private GameOverUI      gameOver;

    private static InteractionManager instance;

    void Awake()
    {
        if ((instance == null) || (instance == this))
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        gameOver = FindObjectOfType<GameOverUI>(true);
    }

    // Update is called once per frame
    void Update()
    {
        bool        clearTooltip = true;
        CursorType  cursor = CursorType.Default;

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

                    // Check if object has actions
                    var actions = h.collider.GetComponents<Action>();

                    if (Input.GetMouseButtonDown(0))
                    {
                        // Check if object has actions
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
                    else
                    {
                        // Check if object has actions
                        if (actions.Length > 0)
                        {
                            foreach (var action in actions)
                            {
                                if (action.enabled)
                                {
                                    if (action.CheckConditions())
                                    {
                                        var tmp = action.GetCursor();
                                        if (tmp != CursorType.Default)
                                        {
                                            cursor = tmp;
                                            break;
                                        }
                                        else
                                        {
                                            cursor = CursorType.Hammer;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Don't check anymore objects (they're hidden behind this one)
                    break;
                }
            }
        }

        Cursor.SetCursor(cursors[(int)cursor], new Vector2(22.0f, 22.0f), CursorMode.Auto);

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

    public static LayerMask GetLayerMask()
    {
        return ~instance.ignoreLayers;
    }
}
