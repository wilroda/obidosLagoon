using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    IEnumerator Start()
    {
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
