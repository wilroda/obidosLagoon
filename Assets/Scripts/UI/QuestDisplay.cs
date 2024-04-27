using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDisplay : MonoBehaviour
{
    public Quest quest;

    [SerializeField] private TextMeshProUGUI    questName;
    [SerializeField] private TextMeshProUGUI    questDescription;
    [SerializeField] private TextMeshProUGUI    timeElement;
    [SerializeField] private ObjectiveDisplay   questItemDisplayPrefab;
    [SerializeField] private Color              incompleteObjectiveColor = Color.red;
    [SerializeField] private Color              completeObjectiveColor = Color.green;

    ContentSizeFitter contentSizeFitter;

    void Start()
    {
        Refresh();        
    }

    [Button("Refresh")]
    public void Refresh()
    {
        if (quest == null) return;
        if (contentSizeFitter == null)
        {
            contentSizeFitter = questDescription.GetComponent<ContentSizeFitter>();
        }

        questName.text = quest.displayName;

        questDescription.text = quest.description;
        questDescription.gameObject.SetActive(quest.description != "");

        contentSizeFitter.SetLayoutVertical();

        timeElement.gameObject.SetActive(quest.hasTimeLimit);
        if (quest.hasTimeLimit)
        {
            float totalTime = quest.maxDuration;
            float currentTime = QuestManager.GetElapsedTime(quest);
            float timeRemaining = totalTime - currentTime;
            if (timeRemaining < 0)
            {
                timeElement.color = incompleteObjectiveColor;
                timeRemaining = 0;
            }
            else
            {
                timeElement.color = completeObjectiveColor;
            }
            timeElement.text = $"Time available: {Helpers.GetTimeString(timeRemaining)}";
        }

        var objectiveDisplays = new List<ObjectiveDisplay>(GetComponentsInChildren<ObjectiveDisplay>());
        var odIndex = 0;

        for (int i = 0; i < quest.nObjectives; i++)
        {
            Color color = incompleteObjectiveColor;
            if (quest.IsComplete(i))
            {
                color = completeObjectiveColor;
            }
            
            if (odIndex >= objectiveDisplays.Count)
            {
                var newOD = Instantiate(questItemDisplayPrefab, transform);
                objectiveDisplays.Add(newOD);
            }

            Token   token = quest.GetToken(i);
            int     tokensRequired = quest.GetQuantity(i);
            int     tokensInInventory = Mathf.Min(InventoryManager.Count(token), tokensRequired);
            objectiveDisplays[odIndex].Set(color, $"{quest.GetObjectiveDisplayName(i)} {tokensInInventory}/{tokensRequired}");

            odIndex++;
        }
        for (int i = quest.nObjectives; i < objectiveDisplays.Count; i++)
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                Destroy(objectiveDisplays[i].gameObject);
            }
            else
            {
                DestroyImmediate(objectiveDisplays[i].gameObject);
            }
#else
            Destroy(objectiveDisplays[i].gameObject);
#endif
        }
    }
}
