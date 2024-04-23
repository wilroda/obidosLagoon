using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSmoothly : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    private bool isRotating = false;

    void Update()
    {
        /* 
        if (Input.GetKeyDown(KeyCode.A) && !isRotating)
        {
            RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) && !isRotating)
        {
            RotateRight();
        } 
        */
    }

    public void RotateRight()
    {
        if(!isRotating)
        {
            StartCoroutine(RotateY(90f));
        }
    }

    public void RotateLeft()
    {
        if(!isRotating)
        {
            StartCoroutine(RotateY(-90f));
        }
    }

    IEnumerator RotateY(float angle)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, angle, 0) * startRotation;
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}