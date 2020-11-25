using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject character;
    public Transform spawn;

    void Start()
    {
        if(!PhotonNetwork.IsConnected) {
            Instantiate(character, spawn.position, character.transform.rotation);
        }
    }

}
