using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    static private SceneHandler instance;

    void Awake()
    {
        if ((instance == null) || (instance == this))
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DOTween.SetTweensCapacity(1000, 100);
    }

    public static void GotoScene(string name)
    {
        Fader.FadeOut(0.5f, () =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(name);
        });
    }

    public static void GotoScene(int id)
    {
        Fader.FadeOut(0.5f, () =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(id);
        });
    }

    public static void Quit()
    {
        Fader.FadeOut(0.5f, () =>
        {
            // Quit the application
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
