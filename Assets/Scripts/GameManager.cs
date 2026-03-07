using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        switch (actorNumber)
        {
            case 1:
                PhotonNetwork.Instantiate("frog", new Vector3(-3.5f, 0, 0), Quaternion.identity);// Jugador 1
                break;
            case 2:
                PhotonNetwork.Instantiate("VirtualGuy", new Vector3(0, 0, 0), Quaternion.identity);// Jugador 2
                break; 
        }
        
            
    }

}
