using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 2;
    public int speed=4;
    
    bool right;
    
     public Vector3 position;
    public Vector3 collisionRight;
    public Vector3 collisionLeft;
    public GameObject player;

   
    void Start()
    {
       // player = GameObject.Find("Player");
        
        right = Player.right;
        collisionRight = new Vector3(1f, 0, 0);
        collisionLeft = new Vector3(-1f, 0, 0);


    }

    void Move()
    {

        if (right)
        {
 position = transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1f,0.5f), speed * Time.deltaTime);
        }
        else
        {
            position = transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(1f, -0.5f), speed * Time.deltaTime);
        }
        
       
    }

    void Update()
    {
        Move();
        

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enimes") {
            GameObject temp = collision.gameObject;
            temp.GetComponent<Enime>().live-=1;
            if (right) temp.GetComponent<Rigidbody2D>().AddForce(collisionRight, ForceMode2D.Impulse);
            else temp.GetComponent<Rigidbody2D>().AddForce(collisionLeft, ForceMode2D.Impulse);
            Destroy(gameObject);
                } 
    }
    
    private void FixedUpdate()
    {
        
        Destroy(GetComponent<Bullet>(), 2);
    }
}
