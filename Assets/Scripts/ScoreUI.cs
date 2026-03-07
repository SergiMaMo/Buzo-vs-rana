
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public class ScoreUI : MonoBehaviourPunCallbacks
{
    public TMP_Text Score1;
    public TMP_Text Score2;

    public GameObject winPanel;
    public GameObject losePanel;

    // Photon llama automáticamente a este método
    // cada vez que cambia una Room Property
    public override void OnRoomPropertiesUpdate(Hashtable properties)
    {
        UpdateUI(properties);
    }
    void UpdateUI(Hashtable properties)
    {
        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Score1"))
            return;
        int myActor = PhotonNetwork.LocalPlayer.ActorNumber;
        int enemyActor = myActor == 1 ? 2 : 1;
        // Leemos la puntuación desde las Room Properties
        Score1.text = PhotonNetwork.CurrentRoom.CustomProperties["Score1"].ToString();
        Score2.text = PhotonNetwork.CurrentRoom.CustomProperties["Score2"].ToString();

        if (!properties.ContainsKey("GameOver")) return;

        bool gameOver = (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameOver"];
        if (!gameOver) return;

        int winner = (int)PhotonNetwork.CurrentRoom.CustomProperties["Winner"];

        if (myActor == winner)
            winPanel.SetActive(true);
        else
            losePanel.SetActive(true);
    }
}
