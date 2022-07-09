using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScoreArchived {
    public string playerName = "";
    public int score = 0;
    public bool alive = true;
}

public class gameStateArchived : MonoBehaviour
{
    public List<PlayerScoreArchived> scores = new List<PlayerScoreArchived>();
    public int roundNumber = 1;
    public int playersLeftAlive = 0;
    public List<GameObject> players = new List<GameObject>();
    PhotonView PV;

    void Awake() {
        PV = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void ReducePlayersAlive() {
        playersLeftAlive--;
        if(playersLeftAlive == 1) {
            PV.RPC("NewRound", RpcTarget.All);
        }
    }

    [PunRPC]
    public void IncrementRounds() {
        roundNumber++;
    }

    [PunRPC]
    public void NewRound() {
        foreach(var player in players) {
            Debug.Log("player.name: " + player.name);
            Destroy(player);
        }
        PhotonNetwork.LoadLevel(1);
        //Load scene
        //put character back at spawn points

    }
}
