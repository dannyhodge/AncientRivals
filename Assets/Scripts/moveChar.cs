using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveChar : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    public bool isGrounded = false;
	public float jumpMoveSpeed = 10f;
	public float currentMoveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector2.right *  Time.deltaTime * currentMoveSpeed);
        }

        if (Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector2.left *  Time.deltaTime * currentMoveSpeed);
        }

        if ((Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) && isGrounded) 
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
			currentMoveSpeed = jumpMoveSpeed;
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
		if(coll.transform.tag == "Ground") {
			isGrounded = true;
			currentMoveSpeed = moveSpeed;
		}
	}

}
