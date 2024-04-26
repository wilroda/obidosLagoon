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

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableGameOver(string reason)
    {
        gameObject.SetActive(true);
        reasonText.text = reason;
        canvasGroup.alpha = 0.0f;
        canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear);
    }
}
