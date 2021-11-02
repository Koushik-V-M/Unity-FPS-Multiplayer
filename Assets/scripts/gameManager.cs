using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class gameManager :MonoBehaviour
{
    public static gameManager instance;

    public matchSettings matchSettings;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.Log("multiple gameobjects present");
        }
        else
        {
            instance = this;
        }
    }

    #region players tracking

    private static Dictionary<string, player> players = new Dictionary<string, player>();

    public static void RegisterPlayer(string _netid, player _player)
    {
        string _playerid = "Player " + _netid;
        players.Add(_playerid, _player);
        _player.transform.name = _playerid;
    }

    public static player getPlayer(string playerid)
    {
        return (players[playerid]);
    }
    public static void unRegisterPlayer(string playerid)
    {
        players.Remove(playerid);
    }

    public static player[] getAllPlayers()
    {
        return players.Values.ToArray();
    }
    #endregion
}
