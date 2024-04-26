using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    [SerializeField] private bool destroyOnFade = true;

    private Image           bubble;
    private TextMeshProUGUI text;
    private CanvasGroup     canvas;
    private Coroutine       cr;
    private Transform       _target;
    private Camera          mainCamera;
    private Canvas          mainCanvas;
    private CanvasScaler    canvasScaler;
    private float           timer;
    private float           offsetY;

    public Transform target => _target;

    private void Start()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        canvasScaler = mainCanvas.GetComponent<CanvasScaler>();
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0.0f;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector2 screenPosition = mainCamera.WorldToViewportPoint(_target.position + Vector3.up * offsetY);

            screenPosition.x = screenPosition.x * canvasScaler.referenceResolution.x;
            screenPosition.y = screenPosition.y * canvasScaler.referenceResolution.y;

            var rt = transform as RectTransform;
            rt.anchoredPosition = screenPosition;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            canvas.alpha = Mathf.Clamp01(canvas.alpha + 4.0f * Time.deltaTime);
        }
        else
        {
            canvas.alpha = Mathf.Clamp01(canvas.alpha - 4.0f * Time.deltaTime);
            if ((canvas.alpha <= 0.0f) && (destroyOnFade))
            {
                Destroy(gameObject);
            }
        }
    }

    public void Set(Transform t, float offsetY, Camera camera)
    {
        _target = t;
        this.offsetY = offsetY;
        mainCamera = camera;
    }

    public void SetDuration(float duration)
    {
        timer = duration;
    }

    public void Set(string speechText, Color bgColor, Color fgColor)
    {
        if (speechText != "")
        {
            canvas = GetComponent<CanvasGroup>();
            bubble = GetComponent<Image>();
            bubble.color = bgColor;
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.color = fgColor;

            text.text = speechText;

            Canvas.ForceUpdateCanvases();

            LayoutRebuilder.ForceRebuildLayoutImmediate(text.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(bubble.rectTransform);
        }
    }
}
