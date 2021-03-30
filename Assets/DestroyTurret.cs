using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{
    GameObject parent; 
    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.FindGameObjectWithTag("TurretDoor");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo){
        if(collisionInfo.gameObject.tag=="Player"){
            Destroy(transform.parent.gameObject);
        }
    }
}
