﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class javelinMoveArchived : MonoBehaviour
{
    public bool hitGround = false;
    public float rotateSpeed = 0.05f;
    public bool goingRight = true;
    public float pushBackSpeed = 500f;
    public bool isStraightVertical = false;
    public gameState GS;
    PhotonView PV;
    public GameObject playerHit;

    void Awake() {
        GS = GameObject.Find("_GameManager").GetComponent<gameState>();
        PV = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if(hitGround == false && isStraightVertical == false) {
            Vector3 temp = transform.eulerAngles;

            if((transform.eulerAngles.z < 90 && transform.eulerAngles.z > 0) || (transform.eulerAngles.z > 270 && transform.eulerAngles.z < 360)) {
                temp.z -= rotateSpeed;
            }
	        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) {
                temp.z += rotateSpeed;         
                }
	    	transform.eulerAngles = temp;
        }  
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.transform.tag != "Player") {
           
           GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           GetComponent<Rigidbody2D>().gravityScale = 0f;
           GetComponent<BoxCollider2D>().isTrigger = true;
           hitGround = true;
           this.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(coll.transform.tag == "Player" && coll.transform.name != "Character") {
            Debug.Log("Player hit: ");
            Debug.Log(coll.transform.gameObject.name);
            playerHit = coll.transform.gameObject;
            
            PV.RPC("PlayerDead", RpcTarget.All);

            coll.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity.normalized * pushBackSpeed);

            this.transform.parent = coll.transform;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<BoxCollider2D>().enabled = false; 
            hitGround = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
	}

    [PunRPC]
    public void PlayerDead() {
        Debug.Log("Player dead: ");
        Debug.Log(playerHit.name);
        GS.ReducePlayersAlive();
        playerHit.GetComponent<BoxCollider2D>().isTrigger = true;
        playerHit.GetComponent<Rigidbody2D>().simulated = false;
        playerHit.GetComponent<moveChar>().enabled = false;
        playerHit.GetComponent<javelinThrow>().enabled = false;
    }
}
