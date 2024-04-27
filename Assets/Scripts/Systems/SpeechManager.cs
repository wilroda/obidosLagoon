using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    [SerializeField] 
    private Camera       mainCamera;
    [SerializeField] 
    private SpeechBubble narratorBubble;
    [SerializeField] 
    private SpeechBubble speechBubblePrefab;
    [SerializeField] 
    private AudioClip   bubbleSound;
    [SerializeField, MinMaxSlider(0.1f, 2.0f), ShowIf("hasSound")] 
    private Vector2     bubbleVolume = Vector2.one;
    [SerializeField, MinMaxSlider(0.1f, 2.0f), ShowIf("hasSound")] 
    private Vector2     bubblePitch = Vector2.one;

    private List<SpeechBubble>  bubbles = new List<SpeechBubble>();
    private Canvas              canvas;
    bool hasSound => bubbleSound != null;

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

    private void PlaySound()
    {
        if (bubbleSound)
        {
            SoundManager.PlaySound(SoundManager.Type.Fx, bubbleSound,
                                   Random.Range(bubbleVolume.x, bubbleVolume.y), Random.Range(bubblePitch.x, bubblePitch.y));
        }

    }

    public static SpeechBubble Say(Transform target, string text, Color bgColor, Color txtColor, float duration, float offsetY, bool managed = true)
    {
        if (target == null)
        {
            instance.narratorBubble.Set(text, bgColor, txtColor);
            instance.narratorBubble.Set(target, offsetY, instance.mainCamera);
            instance.narratorBubble.SetDuration(duration);

            instance.PlaySound();

            return instance.narratorBubble;
        }

        // Check if this is already talking
        if (managed)
        {
            foreach (var s in instance.bubbles)
            {
                if (s.target == target)
                {
                    s.Set(text, bgColor, txtColor);
                    s.SetDuration(duration);

                    instance.PlaySound();

                    return s;
                }
            }
        }

        var bubble = Instantiate(instance.speechBubblePrefab, instance.transform);
        bubble.Set(text, bgColor, txtColor);
        bubble.Set(target, offsetY, instance.mainCamera);
        bubble.SetDuration(duration);

        instance.PlaySound();

        if (managed)
        {
            instance.bubbles.Add(bubble);
        }

        return bubble;
    }

    public static void Clear(SpeechBubble speechBubble)
    {
        instance.bubbles.RemoveAll((s) => s == speechBubble);
        Destroy(speechBubble.gameObject);
    }
}
