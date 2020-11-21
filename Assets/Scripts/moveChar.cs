using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveChar : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public bool isGrounded = false;
    public bool isHanging = false;
    private float gravityScale;
    public bool stopHanging = false;
    public float hangingTimer = 0f;
    public float hangingTime = 0.2f;

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
            }
        }

        if (Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector2.right *  Time.deltaTime * moveSpeed);
        }

        if (Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector2.left *  Time.deltaTime * moveSpeed);
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
            Debug.Log("Hit wall and isgrounded is false");
            isHanging = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
           GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isGrounded = true;
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
            Debug.Log("leave wall");
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            
            stopHanging = true;
        }
	}



}
