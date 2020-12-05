using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class moveChar : MonoBehaviourPun
{
    public enum HangingDirection {Left, Right, None};
    public float maxMoveSpeed = 3f;
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public float sideJumpSpeed = 80f;

    public bool isGrounded = false;
	
    public bool isMovingRight = false;
    public bool isMovingLeft = false;
    public float gravityScale;

    public bool hangingBuffer = false;
    public bool isHanging = false;
    public float hangingTimer = 0f;
    public float hangingTime = 0.2f;
    public HangingDirection hangingDirection = HangingDirection.None;

    PhotonView PV;
    Rigidbody2D RB;

    public float xScale;

    void Awake()
    {
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        RB = GetComponent<Rigidbody2D>();
        if(PhotonNetwork.IsConnected) {
            PV = GetComponent<PhotonView>();
            if(!PV.IsMine) Destroy(RB);
        }
        xScale = transform.localScale.x;
    }

    void FixedUpdate()
    {
    if(PhotonNetwork.IsConnected) {
        if(PV.IsMine) {
            Move();
        }
    }
    else {
        Move();
    }
    }

    void Update() {
        if(PV) {
         if(!PV.IsMine) return;
        }
        if(hangingBuffer) {
            if(hangingTimer < hangingTime) {
                hangingTimer += Time.deltaTime;
            }
            else {
                hangingTimer = 0f;
                hangingBuffer = false;
                isGrounded = false;
                hangingDirection = HangingDirection.None;
            }
        }
        if ((Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) && isGrounded ) 
        {
		    if(!hangingBuffer) isGrounded = false;

            Vector3 forceJump = Vector3.up * jumpSpeed * Time.fixedDeltaTime;
            Vector3 forceRight = Vector3.right * sideJumpSpeed * moveSpeed * Time.fixedDeltaTime;
            Vector3 forceLeft = Vector3.left * sideJumpSpeed * moveSpeed * Time.fixedDeltaTime;
            RB.AddForce(forceJump, ForceMode2D.Impulse);
            
            if(isMovingRight) RB.AddForce(forceRight, ForceMode2D.Impulse);
            if(isMovingLeft) RB.AddForce(forceLeft, ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }

    void AddForce(Vector3 dir) {
        Vector3 forceRight = dir * 1000.0f * moveSpeed * Time.fixedDeltaTime;
        if(RB.velocity.magnitude < maxMoveSpeed) RB.AddForce(forceRight);
    }

    void Move() {
              
    if ((Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.1) )
        {
            Vector3 SpriteScale = transform.localScale;
            SpriteScale.x = -xScale;
            transform.localScale = SpriteScale;
            isMovingLeft = true;
            if(hangingDirection != HangingDirection.Left) {
                AddForce(Vector3.left);
            }
        } else {
            isMovingLeft = false;
        }

    if ((Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.1) )
        {
        Vector3 SpriteScale = transform.localScale;
        SpriteScale.x = xScale;
        transform.localScale = SpriteScale;
        isMovingRight = true;
        if(hangingDirection != HangingDirection.Right) {
	
            AddForce(Vector3.right);
        }
    }
    else {
        isMovingRight = false;
    }     
    }

    void OnCollisionEnter2D(Collision2D coll) {
		if(coll.transform.tag == "Ground" || coll.transform.tag == "Javelin") {
            if(!isHanging) {
		    	isGrounded = true;
            }
            hangingDirection = HangingDirection.None;
		}
        if(isGrounded == false && coll.transform.tag == "Wall") {
            
            if((this.transform.position.x > coll.transform.position.x && transform.localScale.x < 0) 
            || (this.transform.position.x < coll.transform.position.x && transform.localScale.x > 0 )) { 
                if(this.transform.position.x > coll.transform.position.x) hangingDirection = HangingDirection.Left;
                if(this.transform.position.x < coll.transform.position.x) hangingDirection = HangingDirection.Right;
                GetComponent<Rigidbody2D>().gravityScale = gravityScale / 10;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
	}

    void OnCollisionStay2D(Collision2D coll) {
        if(PhotonNetwork.IsConnected) if(!PV.IsMine) return;
		if(coll.transform.tag == "Ground") {
			isGrounded = true;
            
		}
	}

    void OnCollisionExit2D(Collision2D coll) {
        if(PhotonNetwork.IsConnected) if(!PV.IsMine) return;
		if(coll.transform.tag == "Ground") {
			if(!hangingBuffer) isGrounded = false;
		}
        if(coll.transform.tag == "Wall") {
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
	}

}
