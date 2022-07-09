
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore
{
    public string playerName = "";
    public int score = 0;
    public bool alive = true;
}

public class gameState : MonoBehaviour
{
    public List<PlayerScore> scores = new List<PlayerScore>();
    public int roundNumber = 1;
    public int playersLeftAlive = 0;
    public List<GameObject> players = new List<GameObject>();


    void Awake()
    {

    }

    public void ReducePlayersAlive()
    {
        playersLeftAlive--;
        if (playersLeftAlive == 1)
        {
            Debug.Log("Start new round..."); //TODO
        }
    }


    public void IncrementRounds()
    {
        roundNumber++;
    }


    public void NewRound()
    {
        foreach (var player in players)
        {
            Debug.Log("player.name: " + player.name);
            Destroy(player);
        }

        //Load scene
        //put character back at spawn points

    }
}
