
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int MonedasParaGanar = 5;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //crea un hashtable con las puntiaciones asignadas a cada jugador, si se quisiera que hubiera mas usuarios se pondrian mas y listo
            Hashtable scores = new Hashtable {
                { "Score1",0},
                { "Score2",0},
                { "Winner", -1 }         // nadie ha ganado aún
            };
            // Guardamos estas propiedades en la sala
            // Photon las sincroniza automáticamente
            PhotonNetwork.CurrentRoom.SetCustomProperties(scores);
        }
    }
    public void AddPoints(int PlayerNumber) {
        if (!PhotonNetwork.IsMasterClient) return;

        string key = "Score" + PlayerNumber;

        int currentScore =(int) PhotonNetwork.CurrentRoom.CustomProperties[key];

        currentScore++;
        //creamos un hashtable con el dato cambiado
        Hashtable update = new Hashtable{
            {key, currentScore} 
        };

        PhotonNetwork.CurrentRoom.SetCustomProperties(update);

        if (currentScore >= MonedasParaGanar) {

            Hashtable endGame = new Hashtable
            {
                { "GameOver", true },
                { "Winner", PlayerNumber }
            };

            PhotonNetwork.CurrentRoom.SetCustomProperties(endGame);
        }
    }
}
