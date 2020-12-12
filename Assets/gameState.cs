using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerScore {
    public string playerName = "";
    public int score = 0;
    public bool alive = true;
}

public class gameState : MonoBehaviour
{
    public List<PlayerScore> scores = new List<PlayerScore>();
    public int roundNumber = 1;
    public int playersLeftAlive = 0;

    // Update is called once per frame
    void Update()
    {
    //    Debug.Log("Scores:" + scores[0].playerId + ", " + scores[0].score + ", " + scores[0].alive);
    }

    [PunRPC]
    public void ReducePlayersAlive() {
        playersLeftAlive--;
    }

    [PunRPC]
    public void IncrementRounds() {
        roundNumber++;
    }
}
