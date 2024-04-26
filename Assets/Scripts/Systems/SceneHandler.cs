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
        DOTween.KillAll();
        SceneManager.LoadScene(name);
    }

    public static void GotoScene(int id)
    {
        DOTween.KillAll();
        SceneManager.LoadScene(id);
    }
}
