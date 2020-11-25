using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using System.IO;

public class javelinThrow : MonoBehaviour
{
    public GameObject Javelin;              //prefab to throw
    public GameObject javelinSpawnPoint;    //where it spawns from
    public float javelinMoveSpeed = 10f;
    public GameObject JavelinAim;

    PhotonView PV;

    void Start() {

        if(PhotonNetwork.IsConnected) PV = GetComponent<PhotonView>();
        foreach(Transform child in transform) {
            if(child.name == "aimjavelin") JavelinAim = child.gameObject;
        }
        foreach(Transform child in JavelinAim.transform) {
            if(child.name == "javelinSpawnPoint") javelinSpawnPoint = child.gameObject;
        }
    }

    void Update()
    {
        if(PhotonNetwork.IsConnected) if(!PV.IsMine) return;

        if (Input.GetKey("enter") || Input.GetButton("Throw")) 
        {
            GetComponent<moveChar>().currentMoveSpeed = 0;
            float vertMovement = Input.GetAxis ("Vertical");
            float horiMovement= Input.GetAxis ("Horizontal");
            Vector3 temp = JavelinAim.transform.eulerAngles;
	        float horizontalAngle = (Math.Abs(horiMovement) * 90f);
            if(vertMovement > 0) horizontalAngle = 180f - horizontalAngle;
            temp.z = horizontalAngle;
	    	JavelinAim.transform.eulerAngles = temp;
        }

        if (Input.GetKeyUp("enter") || Input.GetButtonUp("Throw")) 
        {
            Quaternion rotation = Quaternion.Euler(javelinSpawnPoint.transform.eulerAngles.x, javelinSpawnPoint.transform.eulerAngles.y * -1, javelinSpawnPoint.transform.eulerAngles.z);
            GameObject jav;
            if(PhotonNetwork.IsConnected) jav = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Javelin"), javelinSpawnPoint.transform.position, rotation);
            else jav = Instantiate(Javelin, javelinSpawnPoint.transform.position, rotation);
            Vector3 targetDir = JavelinAim.transform.rotation * Vector3.down;
            if(JavelinAim.transform.eulerAngles.z > 170 && JavelinAim.transform.eulerAngles.z < 190) jav.GetComponent<javelinMove>().isStraightVertical = true;
            jav.GetComponent<Rigidbody2D>().AddForce(targetDir * javelinMoveSpeed);
            GetComponent<moveChar>().currentMoveSpeed = GetComponent<moveChar>().moveSpeed;
        }
                   
    }

}
