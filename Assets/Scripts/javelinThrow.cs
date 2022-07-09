using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class javelinThrow : MonoBehaviour
{
    public GameObject Javelin;              //prefab to throw
    public GameObject javelinSpawnPoint;    //where it spawns from
    public float javelinMoveSpeed = 10f;
    public GameObject JavelinAim;
    
    public float charMoveSpeed = 5f;
    public bool hasJavelin = true;

    public bool infiniteJavelinsHack = false;

    moveChar moveChar;
    void Start() {
       
        moveChar = GetComponent<moveChar>();
        charMoveSpeed = moveChar.moveSpeed;

        foreach(Transform child in transform) {
            if(child.name == "aimjavelin") JavelinAim = child.gameObject;
        }
        foreach(Transform child in JavelinAim.transform) {
            if(child.name == "javelinSpawnPoint") javelinSpawnPoint = child.gameObject;
        }
    }

    void Update()
    {
        if(hasJavelin) {
        if (Input.GetKey("enter") || Input.GetButton("Throw")) 
        {
            JavelinAim.SetActive(true);
            moveChar.moveSpeed = 0;
            float vertMovement = Input.GetAxis ("Vertical");
            float horiMovement= Input.GetAxis ("Horizontal");
            Vector3 temp = JavelinAim.transform.eulerAngles;
	        float horizontalAngle = ((horiMovement) * 90f);
            if(vertMovement > 0) horizontalAngle = 180f - horizontalAngle;
            temp.z = horizontalAngle;
	    	JavelinAim.transform.eulerAngles = temp;
        }

        if (Input.GetKeyUp("enter") || Input.GetButtonUp("Throw")) 
        {
            JavelinAim.SetActive(false);
            Quaternion rotation = Quaternion.Euler(
                javelinSpawnPoint.transform.eulerAngles.x, 
                javelinSpawnPoint.transform.eulerAngles.y, 
                JavelinAim.transform.eulerAngles.z - 90f);
                
            GameObject jav  = Instantiate(Javelin, javelinSpawnPoint.transform.position, rotation);
            Vector3 targetDir = JavelinAim.transform.rotation * Vector3.down;
            if(JavelinAim.transform.eulerAngles.z > 174 && JavelinAim.transform.eulerAngles.z < 186) jav.GetComponent<javelinMove>().isStraightVertical = true;
            jav.GetComponent<Rigidbody2D>().AddForce(targetDir * javelinMoveSpeed);
            moveChar.moveSpeed = charMoveSpeed;
            if (infiniteJavelinsHack) hasJavelin = true;
            else hasJavelin = false;
        }
        }    
    }

}
