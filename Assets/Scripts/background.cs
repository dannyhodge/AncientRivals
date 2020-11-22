using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    public Sprite mySprite1;
    public Sprite mySprite2;
    public Sprite mySprite3;
    public Sprite mySprite4;
    
    public float time = 0f;
    public float timetoreset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = mySprite1;
    }

    void FixedUpdate(){
        time += Time.deltaTime;
        int rndnum = Random.Range(0,11);

            if(time >= timetoreset)
            {
                if(rndnum >= 0 && rndnum <= 3)
                {
                this.GetComponent<SpriteRenderer>().sprite = mySprite1;
                time = 0;
                }
                if(rndnum >= 4 && rndnum <= 7)
                {
                this.GetComponent<SpriteRenderer>().sprite = mySprite2;
                time = 0;
                }
                else if(rndnum >=8 && rndnum <= 9)
                {
                this.GetComponent<SpriteRenderer>().sprite = mySprite3;
                time = 0;
                }
                else if(rndnum >=10 && rndnum <= 11){
                this.GetComponent<SpriteRenderer>().sprite = mySprite4;
                time = 0;
                }
            }
            
            
        }
    

    // Update is called once per frame
    void Update()
    {
        
        
    }
}

