using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    [SerializeField] private Camera       mainCamera;
    [SerializeField] private SpeechBubble narratorBubble;
    [SerializeField] private SpeechBubble speechBubblePrefab;

    private List<SpeechBubble>  bubbles = new List<SpeechBubble>();
    private Canvas              canvas;

    static SpeechManager instance;
    
    void Start()
    {
        if ((instance == null) || (instance == this))
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        bubbles.RemoveAll((s) => s == null);
    }

    public static SpeechBubble Say(Transform target, string text, Color bgColor, Color txtColor, float duration, float offsetY)
    {
        if (target == null)
        {
            instance.narratorBubble.Set(text, bgColor, txtColor);
            instance.narratorBubble.Set(target, offsetY, instance.mainCamera);
            instance.narratorBubble.SetDuration(duration);
            return instance.narratorBubble;
        }

        // Check if this is already talking
        foreach (var s in instance.bubbles)
        {
            if (s.target == target)
            {
                s.Set(text, bgColor, txtColor);
                s.SetDuration(duration);
                return s;
            }
        }

        var bubble = Instantiate(instance.speechBubblePrefab, instance.transform);
        bubble.Set(text, bgColor, txtColor);
        bubble.Set(target, offsetY, instance.mainCamera);
        bubble.SetDuration(duration);

        instance.bubbles.Add(bubble);

        return bubble;
    }

    public static void Clear(SpeechBubble speechBubble)
    {
        instance.bubbles.RemoveAll((s) => s == speechBubble);
        Destroy(speechBubble.gameObject);
    }
}
