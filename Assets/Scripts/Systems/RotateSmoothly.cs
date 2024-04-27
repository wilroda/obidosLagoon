using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateSmoothly : MonoBehaviour
{
    [SerializeField] private float      rotationSpeed = 1.0f;
    [SerializeField] private Transform  rootObject = null;

    private Binoculars  binoculars;
    private bool        isRotating = false;


    private void Start()
    {
        binoculars = FindObjectOfType<Binoculars>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isRotating && !binoculars.usingBinoculars)
        {
            RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) && !isRotating && !binoculars.usingBinoculars)
        {
            RotateRight();
        } 
    }

    public void RotateRight()
    {
        if(!isRotating)
        {
            StartCoroutine(RotateY(-90f));
        }
    }

    public void RotateLeft()
    {
        if(!isRotating)
        {
            StartCoroutine(RotateY(90f));
        }
    }

    IEnumerator RotateY(float angle)
    {
        isRotating = true;
        Quaternion startRotation = rootObject.rotation;
        Quaternion endRotation = Quaternion.Euler(0, angle, 0) * startRotation;
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rotationSpeed;
            rootObject.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}