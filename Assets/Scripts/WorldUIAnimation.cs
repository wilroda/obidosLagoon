using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIAnimation : MonoBehaviour
{
    [SerializeField] private float      duration = 1.0f;
    [SerializeField] private Vector2    scaleRange = Vector2.one;

    Vector3 originalScale;
    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = originalScale * Mathf.Lerp(scaleRange.x, scaleRange.y, (Mathf.Cos(Mathf.PI * 2.0f * Time.time / duration) + 1) * 0.5f);
    }
}
