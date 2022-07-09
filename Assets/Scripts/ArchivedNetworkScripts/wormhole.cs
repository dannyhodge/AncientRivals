using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormholeArchived : MonoBehaviour
{
    public Transform WormHoleTop;
    public Transform WormHoleBottom;

    void Start()
    {
       WormHoleTop = GameObject.Find("wormholetop").transform;
       WormHoleBottom = GameObject.Find("wormholebottom").transform;
    }

    void Update()
    {
        if(transform.position.y < WormHoleBottom.position.y)  {
            Vector3 temp = transform.position;
	    	temp.y = WormHoleTop.position.y;
		    transform.position = temp;
        }
    }
}   
