using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveChar : MonoBehaviour
{

    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("d"))
        {
            transform.Translate(Vector2.right *  Time.deltaTime * moveSpeed);
        }

    }
}
