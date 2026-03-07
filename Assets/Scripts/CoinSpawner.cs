using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoinSpawner : MonoBehaviour
{



    public Vector2 minPos = new Vector2(-3f, 0.1f);
    public Vector2 maxPos = new Vector2(3f, 2f);
    private GameObject currentPickup;
    public float checkRadius = 0.5f;
    public int maxTries = 20;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnCoin();
        }
    }

    public void SpawnCoin()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        for (float x = minPos.x; x < maxPos.x; x += 0.15f)
        {
            for (float y = minPos.y; y < maxPos.y; y += 0.15f)
            {
                if (Random.value < 0.01f) // probabilidad de moneda
                {
                    Vector2 pos = new Vector2(
                        x + Random.Range(-0.2f, 0.2f),
                        y + Random.Range(-0.2f, 0.2f)
                    );

                    PhotonNetwork.Instantiate(
                        "Coin",
                        pos,
                        Quaternion.identity
                    );
                }
            }
        }
    }
}