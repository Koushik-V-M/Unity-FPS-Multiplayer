using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class joinGame : MonoBehaviour
{
    List<GameObject> roomList = new List<GameObject>();
    private NetworkManager networkManager;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    [SerializeField]
    private Text status;
    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
        refreshRoomList();
    }

    public void refreshRoomList()
    {
        clearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "",true,0,0,OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success,string extendedinfo,List<MatchInfoSnapshot> matchelist)
    {
        status.text = "";
        if (matchelist.Count == 0)
        {
            status.text = "No Rooms Avialable";
            return;
        }

        foreach(MatchInfoSnapshot match in matchelist)
        {
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem roomListItem = roomListItemGO.GetComponent<RoomListItem>();
            if (roomListItem != null)
            {
                roomListItem.Setup(match,JoinRoom);
            }

            roomList.Add(roomListItemGO);

            if (roomList.Count == 0)
            {
                status.text = "No Room Avialable";
            }
        }
    }

    void clearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }
    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        roomList.Clear();
        status.text = "Joining...";
    }
    
}
