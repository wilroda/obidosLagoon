using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private static Fader instance;

    private CanvasGroup canvasGroup;
    private Tween       fadeTween;

    // Start is called before the first frame update
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1.0f;

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
        _FadeIn(0.5f, null);
    }

    public static void FadeIn(float duration, System.Action callback)
    {
        instance._FadeIn(duration, callback);
    }

    private void _FadeIn(float duration, System.Action callback)
    { 
        if ((fadeTween != null) && (!fadeTween.IsPlaying())) fadeTween.Kill();
        if (callback != null)
        {
            fadeTween = canvasGroup.DOFade(0.0f, duration).SetEase(Ease.Linear).OnComplete(() => { callback.Invoke(); });
        }
        else
        {
            fadeTween = canvasGroup.DOFade(0.0f, duration).SetEase(Ease.Linear);
        }
    }

    public static void FadeOut(float duration, System.Action callback)
    {
        instance._FadeOut(duration, callback);
    }

    private void _FadeOut(float duration, System.Action callback)
    {
        if (fadeTween != null) fadeTween.Kill();
        if (callback != null)
        {
            fadeTween = canvasGroup.DOFade(1.0f, duration).SetEase(Ease.Linear).OnComplete(() => { callback.Invoke(); });
        }
        else
        {
            fadeTween = canvasGroup.DOFade(1.0f, duration).SetEase(Ease.Linear);
        }
    }
}
