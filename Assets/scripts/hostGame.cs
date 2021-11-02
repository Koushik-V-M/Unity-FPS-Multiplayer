using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hostGame : MonoBehaviour
{
    [SerializeField]
    private uint roomSize = 6;

    private string roomName;
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker==null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void setRoomName(string name)
    {
        roomName = name;
    }

    public void createRoom()
    {
        if(roomName!="" && roomName != null)
        {
            Debug.Log("name " + roomName + " size " + roomSize);
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "","","",0,0, networkManager.OnMatchCreate);

        }
    }
}
