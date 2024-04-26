using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private Image          backgroundElement;
    [SerializeField] private RectTransform  questContainer;
    [SerializeField] private QuestDisplay   questDisplayPrefab;

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
        int count = QuestManager.openQuestsNotHidden;
        var questDisplays = new List<QuestDisplay>(GetComponentsInChildren<QuestDisplay>());

        if (questDisplays.Count < count)
        {
            for (int i = questDisplays.Count; i < count; i++)
            {
                var newQD = Instantiate(questDisplayPrefab, questContainer);
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
            questDisplays[i].quest = QuestManager.GetActiveQuest(i, false);
            questDisplays[i].Refresh();
        }

        if (backgroundElement)
        {
            backgroundElement.enabled = (count != 0);
        }
    }
}
