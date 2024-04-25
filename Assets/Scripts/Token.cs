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
                _displayName = BuildDisplayName(name);
            }
            return _displayName;
        }
    }

    string BuildDisplayName(string originalName)
    {
        string ret = "";

        for (int i = 0; i < originalName.Length; i++)
        {
            var c = originalName[i];
            if (char.IsUpper(c) && (i > 0))
            {
                ret += " " + char.IsUpper(originalName[i]);
            }
            else if (c == '_') ret += " ";
            else ret += c;
        }

        return ret;
    }
}
