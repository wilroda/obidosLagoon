using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening; 

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    reasonText;

    private CanvasGroup canvasGroup;
    private bool        _gameOverEnabled = false;

    public bool isGameOver => _gameOverEnabled;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        
    }

    public void Retry()
    {
        if (canvasGroup.alpha < 1) return;

        SceneHandler.GotoScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        if (canvasGroup.alpha < 1) return;
        //
    }

    public void EnableGameOver(string reason)
    {
        _gameOverEnabled = true;
        gameObject.SetActive(true);
        reasonText.text = reason;
        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear);
    }
}
