using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    float moveSpeed = 7f;

    Rigidbody2D rb;

    GameObject target;

    Vector2 movedir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        movedir = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2 (movedir.x, movedir.y);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D ( Collider2D col){
        if(col.gameObject.tag=="Player"){
        }
    }
}
