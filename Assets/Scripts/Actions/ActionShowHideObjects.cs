using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public abstract class ActionShowHideObjects : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private GameObject[] objects;
    [SerializeField]
    private float        effectDuration = 0.5f;

    private List<Vector3> originalScales;

    private void Start()
    {
        if (objects != null)
        {
            originalScales = new List<Vector3>();
            foreach (var obj in objects)
            {
                if (obj) originalScales.Add(obj.transform.localScale);
                else originalScales.Add(Vector3.one);
            }
        }
    }

    protected override bool OnRun()
    {
        if (objects != null)
        {
            for (int i = 0; i < objects.Length; i++)            
            {
                var obj = objects[i];
                var originalScale = originalScales[i];

                if (obj)
                {
                    bool nextState = GetFinalState(obj);
                    if (nextState)
                    {
                        Appear(obj, originalScale);
                    }
                    else
                    {
                        Disappear(obj, originalScale);
                    }
                }
            }
        }

        return true;
    }

    protected abstract bool GetFinalState(GameObject obj);

    protected void Appear(GameObject obj, Vector3 originalScale)
    {
        if (effectDuration == 0.0f)
        {
            obj.SetActive(true);
            transform.localScale = originalScale;
        }
        else
        {
            obj.transform.localScale = Vector3.zero;

            obj.SetActive(true);

            // Animate to 150% of the original scale and bounce back to 100%
            obj.transform.DOScale(1.5f * originalScale, effectDuration)
                .SetEase(Ease.OutBack, 4.0f);
        }
    }

    protected void Disappear(GameObject obj, Vector3 originalScale)
    {
        if (effectDuration == 0.0f)
        {
            obj.SetActive(false);
        }
        else
        {
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(obj.transform.DOScale(originalScale * 2.0f, effectDuration * 0.5f)
                .SetEase(Ease.OutExpo));

            mySequence.AppendInterval(-effectDuration * 0.25f);

            mySequence.Append(obj.transform.DOScale(Vector3.zero, effectDuration * 0.5f)
                .SetEase(Ease.InExpo));

            mySequence.OnComplete(() => obj.SetActive(false));

            mySequence.SetAutoKill(true);
        }
    }
}
