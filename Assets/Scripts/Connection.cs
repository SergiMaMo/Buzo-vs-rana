using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class Connection : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al master");
    }
    public void ButtonConnect()
    {
        RoomOptions options = new RoomOptions() { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Conectada a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Hay..." + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores");
    }
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            PhotonNetwork.LoadLevel(1);
            
            Destroy(this);
        }
    }
}
