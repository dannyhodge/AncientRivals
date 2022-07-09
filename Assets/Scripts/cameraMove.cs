using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{

    public float cameraMoveDelay = 2.0f;
    public Transform playerPosition;

    public float timeElapsed = 0f;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(playerPosition==null) playerPosition = GameObject.FindGameObjectWithTag("Player").transform;


        Vector3 lockedNewPosition = new Vector3(playerPosition.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, lockedNewPosition, timeElapsed / cameraMoveDelay);

       // if (timeElapsed < cameraMoveDelay) timeElapsed += Time.deltaTime;



    }
}
