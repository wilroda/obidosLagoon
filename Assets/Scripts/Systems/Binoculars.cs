using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Binoculars : MonoBehaviour
{
    [SerializeField] private Camera binocularCamera; // Reference to the orthographic camera
    [SerializeField] private RectTransform binocularsCanvas; // Canvas with all the binocular elements
    [SerializeField] private RectTransform binocularView; // Raw Image object where the camera is going to be rendered;
    [SerializeField] private RectTransform binocularMask; // UI Image overlay with binoculars shape;
    [SerializeField] private float widthMarginPercentage = 300f;
    [SerializeField] private float heightMarginPercentage = 300;
    [SerializeField] private RectTransform ButtonsUI;

    const float    zoomInDuration = 1f;
    const float    zoomOutDuration = 1f;

    bool        _usingBinoculars = false;
    float       zoomSize = 0f;
    Vector3     worldPosition;
    Coroutine   zoomCR;
    GameOverUI  gameOver;

    public bool usingBinoculars => _usingBinoculars;

    void Start()
    {
        zoomSize = binocularCamera.orthographicSize;

        gameOver = FindObjectOfType<GameOverUI>(true);
    }

    void Update()
    {
        if (!Level.isActive) return;

        if ((Input.GetKeyDown(KeyCode.B) || Input.GetMouseButtonDown(1)))
        {
            ToggleBinoculars();
        }
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        // Cursor.visible = false;

        mousePos.x = Mathf.Clamp(mousePos.x, Screen.width * widthMarginPercentage, Screen.width * (1.0f - widthMarginPercentage));
        mousePos.y = Mathf.Clamp(mousePos.y, Screen.height * heightMarginPercentage, Screen.height * ( 1.0f - heightMarginPercentage));

        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        binocularCamera.transform.position = worldPosition;
        binocularView.transform.position = worldPosition;
        binocularMask.transform.position = worldPosition;
    }

    public void ToggleBinoculars()
    {
        if (gameOver.isGameOver) return;
        if (zoomCR != null) return;

        if (_usingBinoculars) BinocularsViewOff();
        else BinocularsViewOn();
    }

    public void BinocularsViewOn()
    {
        if (gameOver.isGameOver) return;
        if (_usingBinoculars) return;

        _usingBinoculars = true;
        zoomCR = StartCoroutine(ZoomIn(zoomInDuration));        
    }

    public void BinocularsViewOff()
    {
        if (gameOver.isGameOver) return;
        if (!_usingBinoculars) return;

        _usingBinoculars = false;
        zoomCR = StartCoroutine(ZoomOut(zoomOutDuration));        
    }

    IEnumerator ZoomIn(float duration)
    {
        HideUI();
        binocularView.DOScale(1f, zoomInDuration);
        binocularMask.DOScale(1f, zoomInDuration - 0.5f);
        yield return new WaitForSeconds(duration);
        zoomCR = null;
    }

    IEnumerator ZoomOut(float duration)
    {
        ShowUI();
        binocularView.DOScale(0f, 0f);
        binocularMask.DOScale(10f, zoomOutDuration);
        yield return new WaitForSeconds(duration);
        zoomCR = null;
    }

    void HideUI()
    {
        ButtonsUI.DOScaleY(2f, .5f);
    }

    void ShowUI()
    {
        ButtonsUI.DOScaleY(1f, .5f);
    }
}
