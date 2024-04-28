using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Quit Application")]
public class ActionQuitApplication : Action
{
    protected override bool OnRun()
    {
        SceneHandler.Quit();
        
        return true;
    }
}
