using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnBinoculars : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Binoculars binoculars;

    void Start()
    {
        binoculars = FindObjectOfType<Binoculars>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (binoculars)
        {
            if (binoculars.usingBinoculars)
            {
                canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha - 4.0f * Time.deltaTime);
            }
            else
            {
                canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + 4.0f * Time.deltaTime);
            }
        }
    }
}
