using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class moveChar : MonoBehaviourPun, IPunObservable
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

    public bool devTesting = false;

    public float LerpFix = 1f;

    public float maxSpeed = 0.1f;
    public float movementSpeed = 2f;
    public bool jumping = false;
    PhotonView PV;
    Rigidbody2D RB;
    Vector3 selfPos;
    Quaternion selfRot;

    void Start()
    {
        currentMoveSpeed = moveSpeed;
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        RB = GetComponent<Rigidbody2D>();
        if(PhotonNetwork.IsConnected) {
            PV = GetComponent<PhotonView>();
            if(!PV.IsMine) Destroy(RB);
        }
    }

    void Update()
    {
        if(PhotonNetwork.IsConnected) {
            if(PV.IsMine) {
               Move();
            }
            else {
               SmoothPosition();
            }
        }
        else {
            Move();
        }
    }

    void SmoothPosition() {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * LerpFix);
        transform.rotation = Quaternion.Lerp(transform.rotation, selfRot, Time.deltaTime * LerpFix);
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
 
    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
 
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
             if(PV.IsMine){
               stream.SendNext(transform.position);
               stream.SendNext(transform.rotation);
             }
        }
        else {
             if(!PV.IsMine) {
                selfPos = (Vector3)stream.ReceiveNext();
                selfRot = (Quaternion)stream.ReceiveNext();
             }
        }
    }
    void OnCollisionEnter2D(Collision2D coll) {
		if(coll.transform.tag == "Ground" || coll.transform.tag == "Javelin") {
			isGrounded = true;
			currentMoveSpeed = moveSpeed;
            jumping = false;    
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

    void Move() {
        if ((Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < -0.1) ) {
                Quaternion temp = transform.rotation;
	    	    temp.y = 180f;
		        transform.rotation = temp;
        }
        if ((Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0.1) ) {
                Quaternion temp = transform.rotation;
	    	    temp.y = 0f;
		        transform.rotation = temp;
        }

              
    if ((Input.GetKey("a") || Input.GetAxis("Horizontal") < -0.1) )
        {
            isMovingLeft = true;
            if(hangingDirection != HangingDirection.Left) {

                if(isGrounded) {
                    if (RB.velocity.magnitude < (maxSpeed * 1000.0f) )
                    {
                        Vector3 force = Vector3.left * 1000.0f * movementSpeed * Time.fixedDeltaTime;
                        RB.AddForce(force);
                        RB.velocity = RB.velocity.normalized;
                    }
                }
                else {
                    if (RB.velocity.magnitude < (maxSpeed * 100.0f) )
                    {
                        Vector3 force = Vector3.left * 10000.0f * movementSpeed * Time.fixedDeltaTime;
                        RB.AddForce(force);
                        
                    }
                }
            }
        } else {
            isMovingLeft = false;
        }

    if ((Input.GetKey("d") || Input.GetAxis("Horizontal") > 0.1) )
        {
        isMovingRight = true;
        if(hangingDirection != HangingDirection.Right) {
	
            AddForce();
            //   if(isGrounded) {
            //         if (RB.velocity.magnitude < (maxSpeed * 1000.0f) )
            //         {
            //             Vector3 force = Vector3.right * 1000.0f * movementSpeed * Time.fixedDeltaTime;
            //             RB.AddForce(force);
            //             RB.velocity = RB.velocity.normalized;
            //         }
            //     }
            //     else {
            //         if (RB.velocity.magnitude < (maxSpeed * 100.0f) )
            //         {
            //             Vector3 force = Vector3.right * 1000.0f * movementSpeed * Time.fixedDeltaTime;
            //             RB.AddForce(force);
                    
            //         }
            //     }
        }
    }
    else {
        isMovingRight = false;
    }

        if ((Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) && isGrounded ) 
        {
            jumping = true;
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * jumpSpeed);
			currentMoveSpeed = jumpMoveSpeed;

            if(!isHanging) isGrounded = false;
          //  GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }

    void AddForce() {
            Vector3 forceRight = Vector3.right * 1000.0f * movementSpeed * Time.fixedDeltaTime;
            RB.AddForce(forceRight);
            Vector3 forceUp = Vector3.up * jumpSpeed* Time.fixedDeltaTime;
            RB.AddForce(forceUp);
            RB.velocity = RB.velocity.normalized;
    }
}
