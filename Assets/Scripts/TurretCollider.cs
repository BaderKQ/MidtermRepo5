using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollider : MonoBehaviour
{
    public Turret Parent;
    bool startfire=false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(startfire==true){
            Parent.CheckIfTimeToFire();
        }
        
    }

    void OnTriggerEnter2D (Collider2D entered){
        if(entered.gameObject.tag=="Player"){
            startfire=true;
            
        }
    }

    void OnTriggerExit2D (Collider2D exited){
        if(exited.gameObject.tag=="Player")
        startfire=false;
    }
}
