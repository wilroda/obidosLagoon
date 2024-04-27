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
        bool rebuild = false;

        if (questDisplays.Count < count)
        {
            for (int i = questDisplays.Count; i < count; i++)
            {
                var newQD = Instantiate(questDisplayPrefab, questContainer);
                questDisplays.Add(newQD);
                rebuild = true;
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
                rebuild = true;
            }
        }

        for (int i = 0; i < count; i++)
        {
            var q = QuestManager.GetActiveQuest(i, false);
            if (questDisplays[i].quest != q)
            {
                questDisplays[i].quest = q;
                rebuild = true;
            }
            questDisplays[i].Refresh();
        }

        if (backgroundElement)
        {
            backgroundElement.enabled = (count != 0);
        }

        if (rebuild)
        {
            for (int i = 0; i < count; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(questDisplays[i].transform as RectTransform);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(questContainer);
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

            Canvas.ForceUpdateCanvases();
        }
    }
}
