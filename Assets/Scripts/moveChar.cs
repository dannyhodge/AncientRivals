using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class moveChar : MonoBehaviourPun
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

    public float xScale;

    void Awake()
    {
        currentMoveSpeed = moveSpeed;
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
        if(!PV.IsMine) return;
        if ((Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) && isGrounded ) 
        {
		    if(!isHanging) isGrounded = false;
            Vector3 forceRight = Vector3.up * 1000.0f * jumpSpeed * Time.fixedDeltaTime;
            RB.AddForce(forceRight);
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }

    void AddForce(Vector3 dir) {
        Vector3 forceRight = dir * 1000.0f * currentMoveSpeed * Time.fixedDeltaTime;
        if(RB.velocity.magnitude < moveSpeed * 4f) RB.AddForce(forceRight);
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
        if(PhotonNetwork.IsConnected) if(!PV.IsMine) return;
		if(coll.transform.tag == "Ground" || coll.transform.tag == "Wall") {
			isGrounded = true;
		}
	}

    void OnCollisionExit2D(Collision2D coll) {
        if(PhotonNetwork.IsConnected) if(!PV.IsMine) return;
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

}
