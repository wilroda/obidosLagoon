using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

[AddComponentMenu("Actions/Change Level")]
public class ActionChangeLevel : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField, Scene]
    private string scene;

    protected override bool OnRun()
    {
        SceneManager.LoadScene(scene);

        return true;
    }
}
