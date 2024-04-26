using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Give Quest")]
public class ActionGiveQuest : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField]
    private Quest quest;

    protected override bool OnRun()
    {
        QuestManager.Add(quest);

        return true;
    }
}
