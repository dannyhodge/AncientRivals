using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class javelinThrow : MonoBehaviour
{
    public GameObject Javelin;
    public GameObject javelinSpawnPoint;
    public float javelinMoveSpeed = 10f;

    void Start() {
        foreach(Transform child in transform) {
            if(child.name == "javelinSpawnPoint") javelinSpawnPoint = child.gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetButtonDown("Throw")) 
        {
            Debug.Log("angle: " + javelinSpawnPoint.transform.eulerAngles.y);
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, javelinSpawnPoint.transform.eulerAngles.y, transform.rotation.z);
            GameObject jav = Instantiate(Javelin, javelinSpawnPoint.transform.position,rotation);
            if(this.transform.localEulerAngles.y == 180f) jav.GetComponent<Rigidbody2D>().AddForce(Vector2.left * javelinMoveSpeed);
            if(this.transform.localEulerAngles.y == 0f) jav.GetComponent<Rigidbody2D>().AddForce(Vector2.right * javelinMoveSpeed);
        }
    }

    void FixedUpdate() {
   
    }
}
