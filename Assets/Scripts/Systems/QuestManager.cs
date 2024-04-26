using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<Quest>    activeQuests;
    
    private List<Quest>                 completedQuests;
    private Dictionary<Quest, float>    questTimer;

    private static QuestManager instance;

    public static int openQuests
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<QuestManager>();
            if (instance == null) return 0;
            if (instance.activeQuests == null) return 0;
            return instance.activeQuests.Count;
        }
    }
    public static int openQuestsNotHidden
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<QuestManager>();
            if (instance == null) return 0;
            if (instance.activeQuests == null) return 0;

            int count = 0;
            foreach (var q in instance.activeQuests)
            {
                if (q != null)
                {
                    if (!q.hidden) count++;
                }
            }

            return count;
        }
    }

    void Awake()
    {
        DOTween.SetTweensCapacity(1000, 100);

        if ((instance == null) || (instance == this))
        {
            instance = this;

            if (activeQuests == null) activeQuests = new List<Quest>();
            if (completedQuests == null) completedQuests = new List<Quest>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        foreach (var q in activeQuests)
        {
            if (q != null)
            {
                if (q.isComplete)
                {
                    // If binoculars on, disable them
                    var binoculars = FindObjectOfType<Binoculars>();
                    if ((binoculars != null) && (binoculars.usingBinoculars))
                    {
                        binoculars.BinocularsViewOff();
                    }
                    // Move quest to complete, and exit (if there are multiple quests
                    // that can be finished at the same time, the next frame they'll be 
                    // worked on).
                    completedQuests.Add(q);
                    activeQuests.Remove(q);
                    // Find actions to run
                    var qcs = FindObjectsByType<OnQuestCompleted>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                    if (qcs != null)
                    {
                        foreach (var qc in qcs)
                        {
                            if (qc.quest == q)
                            {
                                var actions = qc.GetComponents<Action>();
                                if (actions != null)
                                {
                                    foreach (var action in actions)
                                    {
                                        if (action.enabled)
                                        {
                                            action.Run();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
            }
        }
    }

    public static Quest GetActiveQuest(int i, bool showHidden)
    {
        if (instance == null) return null;

        int count = -1;
        foreach (var q in instance.activeQuests)
        {
            if (q != null)
            {
                if (showHidden) count++;
                else if (!q.hidden) count++;
            }
            if (count == i) return q;

        }

        return null;
    }

    public static float GetTime(Quest quest)
    {
        if (instance == null) return 0.0f;

        if (instance.questTimer == null)
        {
            instance.questTimer = new Dictionary<Quest, float>();
        }

        if (instance.questTimer.TryGetValue(quest, out float t))
        {
            return Time.time - t;
        }

        instance.questTimer[quest] = Time.time;

        return 0.0f;
    }

    public static void Add(Quest quest)
    {
        if (!IsQuestActive(quest) && !IsQuestComplete(quest))
        {
            instance.activeQuests.Add(quest);
        }
    }

    public static bool IsQuestActive(Quest quest)
    {
        return (instance.activeQuests.IndexOf(quest) != -1);
    }
    public static bool IsQuestComplete(Quest quest)
    {
        return (instance.completedQuests.IndexOf(quest) != -1);
    }
}
