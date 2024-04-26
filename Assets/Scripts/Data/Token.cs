using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Token")]
public class Token : ScriptableObject
{
    [SerializeField] private string _displayName;

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
}
