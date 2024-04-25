using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private QuestDisplay questDisplayPrefab;

    void Start()
    {
        Refresh();
    }

    void Update()
    {
        Refresh();            
    }

    [Button("Refresh")]
    void Refresh()
    {
        int count = QuestManager.openQuests;
        var questDisplays = new List<QuestDisplay>(GetComponentsInChildren<QuestDisplay>());

        if (questDisplays.Count < count)
        {
            for (int i = questDisplays.Count; i < count; i++)
            {
                var newQD = Instantiate(questDisplayPrefab, transform);
                questDisplays.Add(newQD);
            }
        }
        else if (questDisplays.Count > count)
        {
            for (int i = count; i < questDisplays.Count; i++)
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying)
                {
                    Destroy(questDisplays[i].gameObject);
                }
                else
                {
                    DestroyImmediate(questDisplays[i].gameObject);
                }
#else
                Destroy(questDisplays[i].gameObject);
#endif
            }
        }

        for (int i = 0; i < count; i++)
        {
            questDisplays[i].quest = QuestManager.GetActiveQuest(i);
            questDisplays[i].Refresh();
        }
    }
}
