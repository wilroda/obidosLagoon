using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public enum TokenState { Have, NotHave };
    public enum QuestState { NotTaken, OnQuest, OnQuestOrCompleted, Completed };

    [System.Serializable]
    public struct TokenCondition
    {
        public TokenState state;
        public Token      token;
    }
    [System.Serializable]
    public struct QuestCondition
    {
        public QuestState state;
        public Quest quest;
    }

    [HorizontalLine(color: EColor.Blue)]
    public bool canRetrigger = false;
    [SerializeField] private AudioClip interactionSound;
    [SerializeField, MinMaxSlider(0.1f, 2.0f), ShowIf("hasSound")] private Vector2 interactionVolume = Vector2.one;
    [SerializeField, MinMaxSlider(0.1f, 2.0f), ShowIf("hasSound")] private Vector2 interactionPitch = Vector2.one;

    [HorizontalLine(color: EColor.Red)]
    [SerializeField] private TokenCondition[] tokenConditions;
    [SerializeField] private QuestCondition[] questConditions;

    bool hasSound => interactionSound != null;

    public bool CheckConditions()
    {
        if (tokenConditions != null)
        {
            foreach (var t in tokenConditions)
            {
                switch (t.state)
                {
                    case TokenState.Have:
                        if (!InventoryManager.HasToken(t.token)) return false;
                        break;
                    case TokenState.NotHave:
                        if (InventoryManager.HasToken(t.token)) return false;
                        break;
                    default:
                        break;
                }
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

            if (interactionSound)
            {
                SoundManager.PlaySound(SoundManager.Type.Fx, interactionSound, Random.Range(interactionVolume.x, interactionVolume.y), Random.Range(interactionPitch.x, interactionPitch.y));
            }
        }
    }

    protected abstract bool OnRun();
}
