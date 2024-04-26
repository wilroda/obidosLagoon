using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static string BuildDisplayName(string originalName)
    {
        string ret = "";

        for (int i = 0; i < originalName.Length; i++)
        {
            var c = originalName[i];
            if (char.IsUpper(c) && (i > 0))
            {
                ret += " " + c;
            }
            else if (c == '_') ret += " ";
            else ret += c;
        }

        return ret;
    }

    public static string GetTimeString(float t)
    {
        int tsec = Mathf.FloorToInt(t);
        int mins = tsec / 60;
        int secs = tsec % 60;

        if (mins > 0)
        {
            return $"{mins:00}m{secs:00}s";
        }

        return $"{secs:00}s";
    }

}
