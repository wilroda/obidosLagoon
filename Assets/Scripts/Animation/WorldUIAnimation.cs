using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldUIAnimation : MonoBehaviour
{
    [SerializeField] private float      duration = 1.0f;
    [SerializeField] private Vector2    scaleRange = Vector2.one;

    Vector3 originalScale;
    bool    init = false;

    private void Update()
    {
        if (!DOTween.IsTweening(transform))
        {
            if (!init)
            {
                originalScale = transform.localScale;
                init = true;
            }
            transform.localScale = originalScale * Mathf.Lerp(scaleRange.x, scaleRange.y, (Mathf.Cos(Mathf.PI * 2.0f * Time.time / duration) + 1) * 0.5f);
        }
    }
}
