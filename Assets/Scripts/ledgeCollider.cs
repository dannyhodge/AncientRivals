﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgeCollider : MonoBehaviour
{
    public GameObject mainCharacter;

    void Start()
    {
        mainCharacter = this.transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("trigger enter: " + coll.name);
    if(mainCharacter.GetComponent<moveChar>().isGrounded == false && coll.transform.tag == "Ground") {
            if((this.transform.position.x > coll.transform.position.x && mainCharacter.GetComponent<moveChar>().isMovingLeft) 
            || (this.transform.position.x < coll.transform.position.x && mainCharacter.GetComponent<moveChar>().isMovingRight) ) { 
                 mainCharacter.GetComponent<moveChar>().isHanging = true;
                 Debug.Log("hit ledge");
                 if(this.transform.position.x > coll.transform.position.x) mainCharacter.GetComponent<moveChar>().hangingDirection 
                    = moveChar.HangingDirection.Left;
                 if(this.transform.position.x < coll.transform.position.x) mainCharacter.GetComponent<moveChar>().hangingDirection 
                    = moveChar.HangingDirection.Right;
                 mainCharacter.GetComponent<moveChar>().isGrounded = true;
                 mainCharacter.GetComponent<Rigidbody2D>().gravityScale = 0;
                 mainCharacter.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                 
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll) {
        Debug.Log("trigger exit: " + coll.name);
        if(coll.transform.tag == "Ground") {
            mainCharacter.GetComponent<Rigidbody2D>().gravityScale = mainCharacter.GetComponent<moveChar>().gravityScale;
            mainCharacter.GetComponent<moveChar>().isGrounded = false;
            mainCharacter.GetComponent<moveChar>().isHanging = false;
            mainCharacter.GetComponent<moveChar>().hangingDirection = moveChar.HangingDirection.None;
        }
	}
}
