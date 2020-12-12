using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpear : MonoBehaviour
{
    public GameObject mainCharacter;

    void Start()
    {
        mainCharacter = this.transform.gameObject;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.transform.tag == "WeaponPickup") {
            mainCharacter.GetComponent<javelinThrow>().hasJavelin = true;
            Destroy(coll.transform.parent.gameObject);
        }   
    }
}
