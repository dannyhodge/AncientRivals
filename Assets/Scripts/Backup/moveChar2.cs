﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class moveChar2 : MonoBehaviour
{
    public enum HangingDirection {Left, Right, None};
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public bool isGrounded = false;
	public float jumpMoveSpeed = 10f;
	public float currentMoveSpeed = 10f;
    public bool isHanging = false;
    public bool isMovingRight = false;
    public bool isMovingLeft = false;
    public float gravityScale;
    public bool stopHanging = false;
    public float hangingTimer = 0f;
    public float hangingTime = 0.2f;
    public HangingDirection hangingDirection = HangingDirection.None;

    PhotonView PV;
    Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = moveSpeed;
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        PV = GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody2D>();

        //if(!PV.IsMine) Destroy(RB);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine) return;
        else Move();

        
    }

    void OnCollisionEnter2D(Collision2D coll) {
		if(coll.transform.tag == "Ground" || coll.transform.tag == "Javelin") {
			isGrounded = true;
			currentMoveSpeed = moveSpeed;
		}
        if(isGrounded == false && coll.transform.tag == "Wall") {
            
            if((this.transform.position.x > coll.transform.position.x && transform.localEulerAngles.y == 180f) 
            || (this.transform.position.x < coll.transform.position.x && transform.localEulerAngles.y == 0f) ) { 
                if(this.transform.position.x > coll.transform.position.x) hangingDirection = HangingDirection.Left;
                if(this.transform.position.x < coll.transform.position.x) hangingDirection = HangingDirection.Right;
                GetComponent<Rigidbody2D>().gravityScale = gravityScale / 10;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isGrounded = true;
            }
        }
	}

    void OnCollisionStay2D(Collision2D coll) {
        if(!PV.IsMine) return;
		if(coll.transform.tag == "Ground" || coll.transform.tag == "Wall") {
			isGrounded = true;
		}
 
	}

    void OnCollisionExit2D(Collision2D coll) {
        if(!PV.IsMine) return;
		if(coll.transform.tag == "Ground") {
			isGrounded = false;
		}
        if(coll.transform.tag == "Wall") {
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            isGrounded = false;
            isHanging = false;
            hangingDirection = HangingDirection.None;
        }
	}

    void Move() {
     if ((Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0.1) )
        {
            isMovingRight = true;
            if(hangingDirection != HangingDirection.Right) {

		    	Quaternion temp = transform.rotation;
	    		temp.y = 0f;
		    	transform.rotation = temp;
                transform.Translate(Vector2.right *  Time.deltaTime * currentMoveSpeed);
            }
        }
        else {
            isMovingRight = false;
        }

        if ((Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < -0.1) )
        {
            isMovingLeft = true;
            if(hangingDirection != HangingDirection.Left) {
                	Quaternion temp = transform.rotation;
	    	    	temp.y = 180f;
		        	transform.rotation = temp;
                    transform.Translate(Vector2.right *  Time.deltaTime * currentMoveSpeed);
            }
        } else {
            isMovingLeft = false;
        }

        if ((Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) && isGrounded ) 
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
			currentMoveSpeed = jumpMoveSpeed;

            if(!isHanging) isGrounded = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }
}
