using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed;  //Enemy speed
    public float StoppingDistance; //When enemy stops 
    public float RetreatDistance; //When enemy runs away 


    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Calls player object 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > StoppingDistance){ //Decides distance between enemy and player, dependent on if when AI stop distance is exceeded

            transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);

        } else if(Vector2.Distance(transform.position, player.position) < StoppingDistance && Vector2.Distance(transform.position, player.position) > RetreatDistance){

            transform.position = this.transform.position;
        
        } else if(Vector2.Distance(transform.position, player.position) < RetreatDistance){

               transform.position = Vector2.MoveTowards(transform.position, player.position, -Speed * Time.deltaTime);

        }
    }
    }
