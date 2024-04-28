using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    RectTransform rect;
    MonoBehaviour idleAnimation;
    public float fullScale = 8.5f;
    public float timeForTitleSCreenToAppear = 0f;
    public float timeToScaleUp = .5f;


    // Start is called before the first frame update
    void Start()
    {
        idleAnimation = GetComponent<IdleAnimation>();
        rect = GetComponent<RectTransform>();    
        StartCoroutine(ShowAfterScenarioScale());
    }

    IEnumerator ShowAfterScenarioScale()
    {
        yield return new WaitForSeconds(timeForTitleSCreenToAppear);
        
        rect.DOScale(fullScale, timeToScaleUp);
        idleAnimation.enabled = true;
    }

    public void ScaleDown()
    {
        rect.DOScale(0f, timeToScaleUp);
    }

}
