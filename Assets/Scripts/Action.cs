using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public enum QuestState { NotTaken, OnQuest, OnQuestOrCompleted, Completed };

    [System.Serializable]
    struct QuestCondition
    {
        public QuestState state;
        public Quest      quest;
    }

    [HorizontalLine(color: EColor.Blue)]
    public bool canRetrigger = false;

    [HorizontalLine(color: EColor.Red)]
    [SerializeField] private Token[] requiredTokens;
    [SerializeField] private Token[] forbiddenTokens;
    [SerializeField] private QuestCondition[] questConditions;

    public bool CheckConditions()
    {
        if (requiredTokens != null)
        {
            foreach (var t in requiredTokens)
            {
                if (!InventoryManager.HasToken(t)) return false;
            }
        }
        if (forbiddenTokens != null)
        {
            foreach (var t in forbiddenTokens)
            {
                if (InventoryManager.HasToken(t)) return false;
            }
        }
        if (questConditions != null)
        {
            foreach (var q in questConditions)
            {
                switch (q.state)
                {
                    case QuestState.NotTaken:
                        if ((QuestManager.IsQuestActive(q.quest)) || (QuestManager.IsQuestComplete(q.quest))) return false;
                        break;
                    case QuestState.OnQuest:
                        if (!QuestManager.IsQuestActive(q.quest)) return false;
                        break;
                    case QuestState.OnQuestOrCompleted:
                        if ((!QuestManager.IsQuestActive(q.quest)) && (!QuestManager.IsQuestComplete(q.quest))) return false;
                        break;
                    case QuestState.Completed:
                        if (!QuestManager.IsQuestComplete(q.quest)) return false;
                        break;
                    default:
                        break;
                }
            }
        }

        if (!CustomConditions()) return false;

        return true;
    }

    protected virtual bool CustomConditions()
    {
        return true;
    }

    public void Run()
    {
        if (!CheckConditions())
        {
            return;
        }

        if (OnRun())
        {
            if (!canRetrigger)
            {
                enabled = false;
            }
        }
    }

    protected abstract bool OnRun();
}
