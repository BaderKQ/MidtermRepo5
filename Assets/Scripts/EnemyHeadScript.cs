using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadScript : MonoBehaviour
{
    public Rigidbody2D player;
    public GameObject DeathEffect; //GameObject death effect 
    public GameObject DeathSound; //Instantiating game object, we need to call the death sound prefab rather than use an audio source 
    private Shake shake; 

    // Start is called before the first frame update
    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>(); //Calls Cam Shake Manager Game Object and calls the Shake Component
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); //Call player's RigidBody2D to manipulate y velo
    }

    // Update is called once per frame
    void Update()
    {

}

    void OnTriggerEnter2D(Collider2D collisionInfo){  
        if(collisionInfo.gameObject.tag=="PlayerFeet"){ //If colliding with feet of player then 
            player.velocity = new Vector2 (player.velocity.x, 30f); //Give player jump boost of 30 (equivalent of jump height in player script)
            shake.CamShake(); //calls cam shake anim 
            Instantiate(DeathEffect, player.transform.position, Quaternion.identity); //Fires the particle system prefab for death 

            Instantiate(DeathSound, player.transform.position, Quaternion.identity); //Fires the Audio game object prefab so player does not destroy enemy with audio source 

            //Destroy(DeathEffect, 3f); //Destroy VFX after 3 seconds, do not destroy with enemy object 

            Destroy(transform.parent.gameObject); //Destroy the enemy object
        } 
        } 
    }


