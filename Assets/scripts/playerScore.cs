using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(player))]
public class playerScore : MonoBehaviour
{
    int lastKills = 0;
    int lastDeaths = 0;

    player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<player>();
        StartCoroutine(syncScore());
    }

    private void OnDestroy()
    {
        syncNow();
    }
    IEnumerator syncScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            syncNow();
        }
        
    }
    
    void syncNow()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.LoggedIn_LoadDataButtonPressed(onDataReceived);
        }
    }

    void onDataReceived(string data)
    {
        if(player.kills<=lastKills && player.deaths <= lastDeaths)
        {
            return;
        }

        int killsSinceLast = player.kills - lastKills;
        int deathsSinceLast = player.deaths - lastDeaths;

        int kills = dataTranslator.dataToKills(data);
        int deaths = dataTranslator.dataToDeaths(data);

        int newKills = kills + player.kills;
        int newdeaths = deaths + player.deaths;

        string newData = dataTranslator.valuesToData(newKills, newdeaths);

        lastKills = player.kills;
        lastDeaths = player.deaths;

        UserAccountManager.instance.LoggedIn_SaveDataButtonPressed(newData);
    }
}
