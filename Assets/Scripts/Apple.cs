using ParrelSync.NonCore;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Apple : MonoBehaviourPun
{
    private AudioSource audioPickUp;
    private bool recogido;
    private void Awake()
    {
        audioPickUp = GetComponent<AudioSource>();
        recogido=false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if(recogido) return;
        if (collision.CompareTag("Player"))
        {
            recogido = true;
            GetComponent<PhotonView>().RPC("ReproducirSonido", RpcTarget.All);
            // Obtenemos el PhotonView del jugador que tocó la moneda
            PhotonView playerView = collision.GetComponentInParent<PhotonView>();
            if (playerView != null)
            {
            
                int actorNumber = playerView.Owner.ActorNumber;
                // Sumamos un punto a ese jugador
                ScoreManager.instance.AddPoints(actorNumber);
            }
            StartCoroutine(DestruirConDelay());
        }
    }
    [PunRPC]
    void ReproducirSonido()
    {
        audioPickUp.Play();
    }
    IEnumerator DestruirConDelay()
    {
        yield return new WaitForSeconds(0.3f);
        PhotonNetwork.Destroy(gameObject);
    }
}
