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

    void Awake()
    {
        DOTween.SetTweensCapacity(1000, 100);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    public static Quest GetActiveQuest(int i)
    {
        if (instance == null) return null;

        return instance.activeQuests[i];
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
}
