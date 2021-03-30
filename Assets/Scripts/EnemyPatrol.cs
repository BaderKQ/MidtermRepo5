using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    Rigidbody2D EnemyBody;
    bool movingright=true;
    bool movingleft=false;
    float movedir=1f;
    float speed=0.05f;
    // Start is called before the first frame update
    void Start()
    {
        EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBody.velocity = new Vector2 ((5f*movedir), EnemyBody.velocity.y);
        }


        

    void OnCollisionEnter2D(Collision2D collisionInfo){
        if(collisionInfo.gameObject.tag=="Wall" && movingright==true){
            movedir=-1f;
            EnemyBody.velocity = new Vector2 (speed, EnemyBody.velocity.y);
            speed = speed * movedir;
            movingright=false; 
            movingleft=true;
        } else if(collisionInfo.gameObject.tag=="Wall" && movingright==false && movingleft==true){
            EnemyBody.velocity = new Vector2 (speed, EnemyBody.velocity.y);
            movedir=1f;
            speed = speed * movedir;
            movingright=true; 
    }
}
}
