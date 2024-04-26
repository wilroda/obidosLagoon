using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/On Quest Failed")]
public class OnQuestFailed : OnQuestStateChange
{
    public override bool IsComplete()
    {
        return false;
    }
}
