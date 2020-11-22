using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class javelinMove : MonoBehaviour
{

    public bool hitGround = false;
    public float rotateSpeed = 0.05f;
    void Start() {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(transform.localEulerAngles.z < 45f && hitGround == false) {
            Quaternion temp = transform.rotation;
	        temp.z -= rotateSpeed;
	    	transform.rotation = temp;
        }
 
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(coll.transform.tag != "Character") {
           GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           GetComponent<Rigidbody2D>().gravityScale = 0f;
           GetComponent<BoxCollider2D>().isTrigger = true;
           hitGround = true;
        }
	}
}
