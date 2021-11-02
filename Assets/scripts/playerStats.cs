using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public Text killCount;
    public Text deathCount;
    // Start is called before the first frame update
    void Start()
    {
        if (UserAccountManager.IsLoggedIn) 
        { 
            UserAccountManager.instance.LoggedIn_LoadDataButtonPressed(onReceivedData);
        }
    }

    void onReceivedData(string data)
    {
        if (killCount == null || deathCount == null)
        {
            return;
        }
        killCount.text = dataTranslator.dataToKills(data) + " Kills";
        deathCount.text = dataTranslator.dataToDeaths(data) + " Deaths";
    }
}
