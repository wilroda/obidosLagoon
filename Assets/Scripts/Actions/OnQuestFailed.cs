using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Actions/_Conditions/On Quest Failed")]
public class OnQuestFailed : OnQuestStateChange
{
    public override bool IsComplete()
    {
        return false;
    }
}
