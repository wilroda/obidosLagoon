using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    bool _isActive = false;

    static Level instance;
    IEnumerator Start()
    {
        if ((instance != null) && (instance != this))
        {
            Destroy(gameObject);
            yield break;
        }
        instance = this;
        _isActive = false;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack, 4.0f);
        yield return new WaitForSeconds(0.75f);
        _isActive = true;
    }

    public static bool isActive => instance._isActive;
}
