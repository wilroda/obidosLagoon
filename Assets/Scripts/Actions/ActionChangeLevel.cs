using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Change Level")]
public class ActionChangeLevel : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField, Scene]
    private string scene;

    protected override bool OnRun()
    {
        SceneHandler.GotoScene(scene);

        return true;
    }
}
