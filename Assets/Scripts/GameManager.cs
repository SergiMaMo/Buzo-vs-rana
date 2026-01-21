using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// librerías de Photon
using Photon.Pun;

// Script de Unity (1 referencia de recurso)
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // La instanciación la hace Photon.
        // Ojo el nombre lo busca en una carpeta que se llama Resources
        // Y es ahí donde tenemos que meter nuestros prefabs
        if (PhotonNetwork.IsMasterClient) // Si soy el máster, soy el jugador 1.
            PhotonNetwork.Instantiate("Frog", new Vector3(-9, -8, 0), Quaternion.identity);
        else
            PhotonNetwork.Instantiate("VirtualGuy", new Vector3(0, -8, 0), Quaternion.identity);
    }
}
