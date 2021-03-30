using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myBody; 
    public float jumpHeight;
    public float boostamount;
    private bool onFloor=true;

    public Transform spawnpoint;
    public Transform spawnpoint2;
    public Transform spawnpoint3; 
    public Transform spawnpoint4;
    public Transform spawnpoint5;
    public Transform spawnpoint6;

    public float DashSpeed;
    bool hasDashed = false;
    bool downDash=false;
    bool EnteredPoint1 = false;
    bool EnteredPoint2 = false; 
    bool EnteredPoint3 = false;
    bool EnteredPoint4 = false;
    bool EnteredPoint5 = false;
    bool EnteredPoint6 = false;

    private bool CanDoubleJump = false; 
    private Shake shake; 

    public GameObject dashEffect;
    public GameObject doorEffect;
    public GameObject groundEffect;
    public GameObject keyUnlockEffect;
    public GameObject CheckPointEffect;
    BoxCollider2D enemyboxcollider; 

    public AudioClip JumpSound;
    public AudioClip Death;
    public AudioClip DashSound;
    public AudioClip SmashSound;
    public AudioClip groundShake;
    public AudioClip GotKey;
    AudioSource ticksound;

    public EnemyMovement DoesEnemyDie;


    public float SightDistance;
    public Rigidbody2D Key;

    private bool UnlockedMech1=false;
    private bool UnlockedMech2=false;

    SpriteRenderer CheckpointColor;



    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>(); //Call RigidBody component 
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>(); //Calls Cam Shake Manager Game Object and calls the Shake Component
        ticksound = gameObject.GetComponent<AudioSource>();
        Key = GameObject.FindGameObjectWithTag("Key").GetComponent<Rigidbody2D>(); //Call player's RigidBody2D to manipulate y velo
        enemyboxcollider = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BoxCollider2D>();
        CheckpointColor = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveKeys();
        Jumping();
        Dashing();
        GroundShake();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, SightDistance);
        if(hit.collider !=null){
            if(hit.collider.tag=="Key"){
                Key.velocity = new Vector2 (Key.transform.position.x, Key.transform.position.y + jumpHeight);

            }

        }

    }


    
    void OnCollisionEnter2D(Collision2D collisionInfo){ //this helps reset the jump , this void collision also detects enemy projectiles for witchtime 
        if(collisionInfo.gameObject.tag=="Floor"){ //if you collide with the tagged floor object 
            onFloor=true; //Then you are on the floor 
            hasDashed=false;
            downDash=false;
    }
        if(collisionInfo.gameObject.tag=="Door" && hasDashed==true){
            shake.CamShake(); //calls cam shake anim
            ticksound.PlayOneShot(SmashSound, 0.7f);
            Instantiate(doorEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            Destroy(collisionInfo.gameObject);
        }

        if(collisionInfo.gameObject.tag=="Key"){
            Destroy(collisionInfo.gameObject);
            Instantiate(keyUnlockEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            UnlockedMech1=true;
             ticksound.PlayOneShot(GotKey, 0.7f);
        }

        if(collisionInfo.gameObject.tag=="SecondKey"){
            Destroy(collisionInfo.gameObject);
            Instantiate(keyUnlockEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            UnlockedMech2=true;
             ticksound.PlayOneShot(GotKey, 0.7f);
        }

        if(collisionInfo.gameObject.tag=="DownSmash" && downDash==true){
            shake.CamShake(); //calls cam shake anim
            ticksound.PlayOneShot(SmashSound, 0.7f);
            Instantiate(doorEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            Destroy(collisionInfo.gameObject);
        }  else if(collisionInfo.gameObject.tag=="DownSmash" && downDash==false){
             onFloor=true; //Then you are on the floor 
        }

        if(collisionInfo.gameObject.tag=="Lava"  &&  EnteredPoint1==true){
            transform.position = spawnpoint.position;
             shake.CamShake(); //calls cam shake anim
             Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Lava" && EnteredPoint2==true){
             shake.CamShake(); //calls cam shake anim
             Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            transform.position = spawnpoint2.position;
            ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Lava" && EnteredPoint3==true){
             shake.CamShake(); //calls cam shake anim
             Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            transform.position = spawnpoint3.position;
            ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Lava" && EnteredPoint4==true){
             shake.CamShake(); //calls cam shake anim
             Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            transform.position = spawnpoint4.position;
            ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Lava" && EnteredPoint5==true){
             shake.CamShake(); //calls cam shake anim
             Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            transform.position = spawnpoint5.position;
            ticksound.PlayOneShot(Death, 0.7f);
        }  else if(collisionInfo.gameObject.tag=="Lava" && EnteredPoint6==true){
             shake.CamShake(); //calls cam shake anim
             Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
            transform.position = spawnpoint6.position;
            ticksound.PlayOneShot(Death, 0.7f);
        }

        if(collisionInfo.gameObject.tag=="SwitchOff"){
            myBody.velocity = new Vector2 (myBody.velocity.x, myBody.velocity.y + boostamount); //X axis velo is unaffected, but Y axis is affected by public Jump Height float. Allow double jump.
            Destroy(collisionInfo.gameObject);
        }

        if(collisionInfo.gameObject.tag=="Enemy" && EnteredPoint1==true){
             transform.position = spawnpoint.position;
              shake.CamShake(); //calls cam shake anim
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Enemy" && EnteredPoint2==true){
             shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint2.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Enemy" && EnteredPoint3==true){
             shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint3.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Enemy" && EnteredPoint4==true){
             shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint4.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Enemy" && EnteredPoint5==true){
             shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint5.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisionInfo.gameObject.tag=="Enemy" && EnteredPoint6==true){
             shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint6.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
 
        }
    }


    void OnTriggerEnter2D(Collider2D collisioninfo){
 
        if(collisioninfo.gameObject.tag=="Checkpoint"){
             CheckpointColor.color = Color.blue;
        }

         if(collisioninfo.gameObject.tag=="BulletCollider" && EnteredPoint3==true){
             shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint3.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisioninfo.gameObject.tag=="BulletCollider" && EnteredPoint4==true){
            shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint4.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisioninfo.gameObject.tag=="BulletCollider" && EnteredPoint5==true){
            shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint5.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        } else if(collisioninfo.gameObject.tag=="BulletCollider" && EnteredPoint6==true){
            shake.CamShake(); //calls cam shake anim
            transform.position = spawnpoint6.position;
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(Death, 0.7f);
        }


        if(collisioninfo.gameObject.tag=="SpawnPoint1"){
            EnteredPoint1=true;
        } else if(collisioninfo.gameObject.tag=="SpawnPoint2"){
            EnteredPoint1=false; 
            EnteredPoint2= true;
        } else if(collisioninfo.gameObject.tag=="SpawnPoint3"){
            EnteredPoint1=false; 
            EnteredPoint2= false;
            EnteredPoint3=true;
        } else if(collisioninfo.gameObject.tag=="SpawnPoint4"){
            EnteredPoint1=false; 
            EnteredPoint2=false;
            EnteredPoint3=false;
            EnteredPoint4=true;
        } else if(collisioninfo.gameObject.tag=="SpawnPoint5"){
            EnteredPoint1=false; 
            EnteredPoint2=false;
            EnteredPoint3=false;
            EnteredPoint4=false;
            EnteredPoint5=true;
    } else if(collisioninfo.gameObject.tag=="SpawnPoint6"){
            EnteredPoint1=false; 
            EnteredPoint2=false;
            EnteredPoint3=false;
            EnteredPoint4=false;
            EnteredPoint5=false;
            EnteredPoint6=false;
    }
    }

    void MoveKeys(){
         if(Input.GetKey(KeyCode.D)){ //MoveRight 
            myBody.velocity = new Vector2( 10, myBody.velocity.y );//Moves 10 units to the right with velocity at Y constant 
        } else if (Input.GetKey(KeyCode.A)){
            myBody.velocity = new Vector2 (-10, myBody.velocity.y); //Moves 10 units to the left with velocity at Y constant 
        }  
    }

    void Jumping(){
        if(onFloor && myBody.velocity.y > 0 ){ //If you are above the Y axis, aka while in jump 
            onFloor = false; //You are not on the floor 
        }

        if(onFloor && Input.GetKeyDown(KeyCode.W)){ //Jump Start Code, this says if you're on the floor and you press W then  
            myBody.velocity = new Vector2 (myBody.velocity.x, jumpHeight); //X axis velo is unaffected, but Y axis is affected by public Jump Height float 
            CanDoubleJump= true; //Window to double jump is set to true w
            ticksound.PlayOneShot(JumpSound, 0.7f);

        } else if(!onFloor && Input.GetKeyDown(KeyCode.W) && CanDoubleJump==true){ //If not on floor, and button is pressed while double jump is true then 
             myBody.velocity = new Vector2 (myBody.velocity.x, jumpHeight); //X axis velo is unaffected, but Y axis is affected by public Jump Height float. Allow double jump.
             CanDoubleJump=false; //Set to false once happened 
             ticksound.PlayOneShot(JumpSound, 0.7f);
        }

        if(!onFloor && !Input.GetKeyDown(KeyCode.W) && !CanDoubleJump){ //Therefore if you are not on the floor and not pressing the button, and double jump is false
            myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y); //Both axes are considered constant and unaffected 
        }
    }

    void Dashing(){
         if(Input.GetKeyDown(KeyCode.RightArrow) && !onFloor && hasDashed==false && UnlockedMech1==true){ //If player presses right arrow and not on floor 
            myBody.velocity = new Vector2(myBody.velocity.x + DashSpeed , myBody.velocity.y); //X Velo is multiplied by public dashspeed amount manipulated in inspector
            shake.CamShake(); //calls cam shake anim
            ticksound.PlayOneShot(DashSound, 0.7f); //Plays audiosource and clip 
            hasDashed=true; //Player has dashed 
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 

        } else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) && !onFloor && hasDashed==true && UnlockedMech1==true){ //If player presses right arrow again after Dashing  
            myBody.velocity = new Vector2(myBody.velocity.x , myBody.velocity.y); //X and Y velo are unaffected

        } else if(Input.GetKeyDown(KeyCode.LeftArrow) && !onFloor && hasDashed==false && UnlockedMech1==true){
            shake.CamShake(); 
            ticksound.PlayOneShot(DashSound, 0.7f);
            hasDashed=true; //Player has dashed 
            myBody.velocity = new Vector2(myBody.velocity.x + (DashSpeed * -1f), myBody.velocity.y);
            Instantiate(dashEffect, transform.position, Quaternion.identity);
        }
    }

    void GroundShake(){
        if(Input.GetKeyDown(KeyCode.DownArrow) && !onFloor && UnlockedMech2==true){
            downDash=true;
            shake.CamShake();
            myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y - 30f); 
            Instantiate(groundEffect, transform.position, Quaternion.identity);
            Instantiate(dashEffect, transform.position, Quaternion.identity); //instantiates the particle system 
             ticksound.PlayOneShot(groundShake, 0.7f);
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && onFloor && UnlockedMech2==true && downDash==true){
             myBody.velocity = new Vector2(myBody.velocity.x , myBody.velocity.y); //X and Y velo are unaffected
        }
    }
}



