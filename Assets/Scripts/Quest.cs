using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [System.Serializable]
    struct QuestItem
    {
        public Token    token;
        public int      quantity;
    }
    [SerializeField] 
    private string      _displayName;
    [SerializeField, TextArea] 
    private string      _description;
    [SerializeField]
    private QuestItem[] _items;
    [SerializeField]
    private bool        _timeLimit;
    [SerializeField, ShowIf("_timeLimit")]
    private int         _timeInSeconds;

    public string displayName
    {
        get
        {
            if (_displayName == "") 
            {
                _displayName = Helpers.BuildDisplayName(name);
            }
            return _displayName;
        }
    }

    public string description => _description;

    public int nObjectives => (_items == null) ? (0) : (_items.Length);
    public bool hasTimeLimit => _timeLimit;
    public int maxDuration => _timeInSeconds;

    public Token GetToken(int index)
    {
        return _items[index].token;
    }
    public int GetQuantity(int index)
    {
        return _items[index].quantity;
    }

    public bool IsComplete(int index)
    {
        Token token = _items[index].token;

        if (InventoryManager.Count(token) < _items[index].quantity) return false;

        return true;
    }

    public string GetObjectiveDisplayName(int index)
    {
        return GetToken(index).displayName;
    }
}
