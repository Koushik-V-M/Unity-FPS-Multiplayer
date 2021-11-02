using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataTranslator : MonoBehaviour
{
    private static string killsTag = "[KILLS]";
    private static string deathsTag = "[DEATHS]";

    public static string valuesToData(int kills,int deaths)
    {
        return killsTag + kills + "/" + deathsTag + deaths;
    }
    public static int dataToKills(string data)
    {
        return int.Parse(dataToValue(data, killsTag));
    }

    public static int dataToDeaths(string data)
    {
        return int.Parse(dataToValue(data, deathsTag));
    }

    private static string dataToValue(string data,string tag)
    {
        string[] peices = data.Split('/');
        foreach (string peice in peices)
        {
            if (peice.StartsWith(tag))
            {
                return peice.Substring(tag.Length);
            }
        }
        Debug.LogError("invalid data");
        return "";
    }
}
