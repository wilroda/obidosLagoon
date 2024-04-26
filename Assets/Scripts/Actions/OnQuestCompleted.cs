using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/On Quest Complete")]
public class OnQuestCompleted : OnQuestStateChange
{
    public override bool IsComplete()
    {
        return true;
    }
}
