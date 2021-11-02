using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreboard : MonoBehaviour
{
    [SerializeField]
    GameObject playerScoreboardItem;

    [SerializeField]
    Transform playerScoreboardList;
    private void OnEnable()
    {
        player[] players = gameManager.getAllPlayers();
        foreach(player _player in players)
        {
            GameObject itemGO=(GameObject)Instantiate(playerScoreboardItem, playerScoreboardList);
            playerScoreboardItem item = itemGO.GetComponent<playerScoreboardItem>();
            if (item != null)
            {
                item.Setup(_player.username, _player.kills, _player.deaths);
            }
        }
    }

    private void OnDisable()
    {
        foreach(Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}
