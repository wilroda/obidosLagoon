using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Binoculars : MonoBehaviour
{
    public Camera binocularCamera; // Reference to the orthographic camera
    public RectTransform binocularsCanvas; // Canvas with all the binocular elements
    public RectTransform binocularView; // Raw Image object where the camera is going to be rendered;
    public RectTransform binocularMask; // UI Image overlay with binoculars shape;

    float widthMargin = 300f;
    public float binocularWidth = 300f;
    float heightMargin;
    public float binocularHeight = 190f;

    public bool usingBinoculars = false;
    bool usingZoom = false;
    float zoomSize = 0f;
    public float zoomInDuration = 1f;
    public float zoomOutDuration = 1f;
    
    Vector3 worldPosition;

    public RectTransform ButtonsUI;
    public RectTransform QuestUI;

    void Start()
    {
        zoomSize = binocularCamera.orthographicSize;
        heightMargin = Screen.height - binocularHeight;
        widthMargin = Screen.width - binocularWidth;
    }

    void Update()
    {        
        if(Input.GetKeyDown(KeyCode.B) && usingBinoculars && !usingZoom)
        {
            BinocularsViewOff();
        } else if(Input.GetKeyDown(KeyCode.B) && !usingBinoculars && !usingZoom)
        {
            BinocularsViewOn();
        } 
        
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        // Cursor.visible = false;

        if(mousePos.x < binocularWidth)
        {
            mousePos.x = binocularWidth;
        }

        if(mousePos.x > widthMargin)
        {
            mousePos.x = widthMargin;
        }

        if(mousePos.y > heightMargin)
        {
            mousePos.y = heightMargin;
        }

        if(mousePos.y < binocularHeight)
        {
            mousePos.y = binocularHeight;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        binocularCamera.transform.position = worldPosition;
        binocularView.transform.position = worldPosition;
        binocularMask.transform.position = worldPosition;
    }

    public void BinocularsViewOn()
    {
        usingBinoculars = true;
        StartCoroutine(ZoomIn(zoomInDuration));        
    }

    public void BinocularsViewOff()
    {
        usingBinoculars = false;
        StartCoroutine(ZoomOut(zoomOutDuration));        
    }

    IEnumerator ZoomIn(float duration)
    {
        usingZoom = true;
        HideUI();
        binocularView.DOScale(1f, zoomInDuration);
        binocularMask.DOScale(1f, zoomInDuration - 0.5f);
        yield return new WaitForSeconds(duration);
        usingZoom = false;
        
    }

    IEnumerator ZoomOut(float duration)
    {
        usingZoom = true;
        ShowUI();
        binocularView.DOScale(0f, 0f);
        binocularMask.DOScale(10f, zoomOutDuration);
        yield return new WaitForSeconds(duration);
        usingZoom = false;
        
    }

    void HideUI()
    {
        ButtonsUI.DOScaleY(2f, .5f);
        QuestUI.DOScaleX(0f, .2f);
    }

    void ShowUI()
    {
        ButtonsUI.DOScaleY(1f, .5f);
        QuestUI.DOScaleX(1f, .2f);
    }

   
}