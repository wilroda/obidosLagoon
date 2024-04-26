using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleAnimation : MonoBehaviour
{
    bool scaleUp = true;
    bool scaleDown = false;
    public float animationSpeed = 0.25f;
    public float timeBetweenScaleIntervals = 1f;
    public float minScale = 1f;
    public float maxScale = 2f;

    bool rotatePositive = true;
    bool rotateNegative = false;
    Vector3 positiveRotation;
    Vector3 negativeRotation;
    Vector3 currentRotation;
    Vector3 originalScale;
    public float timeBetweenRotateIntervals = 1f;
    public float minRotation = -10f;
    public float maxRotation = 10f;

    void Start()
    {
        currentRotation = transform.localEulerAngles;
        originalScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        positiveRotation = new Vector3(Random.Range(minRotation, minRotation - 0.5f), currentRotation.y, currentRotation.z);
        negativeRotation = new Vector3(Random.Range(maxRotation, maxRotation - 0.5f), currentRotation.y, currentRotation.z);

        if(scaleUp)
        {
            scaleUp = false;
            StartCoroutine(ScalingUp());
        } else if (scaleDown)
        {
            scaleDown = false;
            StartCoroutine(ScalingDown());
        }

        if(rotatePositive)
        {
            rotatePositive = false;
            StartCoroutine(RotatePositive());
        } else if (rotateNegative)
        {
            rotateNegative = false;
            StartCoroutine(RotateNegative());
        }
        

    }

    IEnumerator ScalingUp()
    {
        transform.DOScale(originalScale * Random.Range(maxScale, maxScale - 0.5f), animationSpeed);
        yield return new WaitForSeconds(timeBetweenScaleIntervals);
        scaleDown = true;
    }

    IEnumerator ScalingDown()
    {
        transform.DOScale(originalScale * Random.Range(minScale, minScale - 0.5f), animationSpeed);
        yield return new WaitForSeconds(timeBetweenScaleIntervals);
        scaleUp = true;
    }

    IEnumerator RotatePositive()
    {
        transform.DOLocalRotate(positiveRotation, animationSpeed, RotateMode.Fast);
        yield return new WaitForSeconds(timeBetweenRotateIntervals);
        rotateNegative = true;
    }
    IEnumerator RotateNegative()
    {
        transform.DOLocalRotate(negativeRotation, animationSpeed, RotateMode.Fast);
        yield return new WaitForSeconds(timeBetweenRotateIntervals);
        rotatePositive = true;
    }

}
