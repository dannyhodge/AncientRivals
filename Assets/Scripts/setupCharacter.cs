﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setupCharacter : MonoBehaviour
{
    GameManager GM;

    void Start()
    {
        // PV = GetComponent<PhotonView>();
        // GM = GameObject.Find("_GameManager").GetComponent<GameManager>();
        // Sprite sprite1 = GM.GetCharacterSprite();
    
        // if(PV.IsMine) {
        //     PV.RPC("GetSprite", RpcTarget.All, sprite1);
        // }
    }

    // [PunRPC]
    // public void GetSprite(Sprite sprite1) {
    //     this.GetComponent<SpriteRenderer>().sprite = sprite1;
    //     Debug.Log("Running");
    // }

}
