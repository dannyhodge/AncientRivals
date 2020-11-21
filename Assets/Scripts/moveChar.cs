using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveChar : MonoBehaviour
{
    public enum HangingDirection {Left, Right, None};
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public bool isGrounded = false;
    public bool isHanging = false;
    public bool isMovingRight = false;
    public bool isMovingLeft = false;
    private float gravityScale;
    public bool stopHanging = false;
    public float hangingTimer = 0f;
    public float hangingTime = 0.2f;
    public HangingDirection hangingDirection = HangingDirection.None;

    // Start is called before the first frame update
    void Start()
    {
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopHanging) {
            hangingTimer += Time.deltaTime;
            if(hangingTimer > hangingTime) {
                isHanging = false;
                stopHanging = false;
                isGrounded = false;
                hangingTimer = 0f;
                hangingDirection = HangingDirection.None;
            }
        }

        if ((Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0.05) )
        {
            isMovingRight = true;
            
            if(hangingDirection != HangingDirection.Right) {

		    	Quaternion temp = transform.rotation;
	    		temp.y = 0f;
		    	transform.rotation = temp;
                transform.Translate(Vector2.right *  Time.deltaTime * moveSpeed);
            }
        }
        else {
            isMovingRight = false;
        }

        if ((Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < -0.05) )
        {
            isMovingLeft = true;
            if(hangingDirection != HangingDirection.Left) {
                	Quaternion temp = transform.rotation;
	    	    	temp.y = 180f;
		        	transform.rotation = temp;
                    transform.Translate(Vector2.right *  Time.deltaTime * moveSpeed);
            }
        } else {
            isMovingLeft = false;
        }

        if ((Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) && isGrounded ) 
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);

            if(!isHanging) isGrounded = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
        
    }

    void OnCollisionEnter2D(Collision2D coll) {
		if(coll.transform.tag == "Ground") {
			isGrounded = true;
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

    void OnTriggerEnter2D(Collider2D coll) {
    if(isGrounded == false && coll.transform.tag == "Ledge") {
            Debug.Log(transform.localEulerAngles.y);
            if((this.transform.position.x > coll.transform.position.x && isMovingLeft) 
            || (this.transform.position.x < coll.transform.position.x && isMovingRight) ) { 
                 isHanging = true;
                 if(this.transform.position.x > coll.transform.position.x) hangingDirection = HangingDirection.Left;
                 if(this.transform.position.x < coll.transform.position.x) hangingDirection = HangingDirection.Right;
                 GetComponent<Rigidbody2D>().gravityScale = 0;
                 GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isGrounded = true;
            }
        }
    }
    void OnCollisionStay2D(Collision2D coll) {
		if(coll.transform.tag == "Ground" || coll.transform.tag == "Wall") {
			isGrounded = true;
		}
 
	}

    void OnCollisionExit2D(Collision2D coll) {
		if(coll.transform.tag == "Ground") {
			isGrounded = false;
		}
        if(coll.transform.tag == "Wall") {
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            stopHanging = true;
        }
	}

        void OnTriggerExit2D(Collider2D coll) {
		if(coll.transform.tag == "Ground") {
			isGrounded = false;
		}
        if(coll.transform.tag == "Ledge") {
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            stopHanging = true;
        }
	}

}
