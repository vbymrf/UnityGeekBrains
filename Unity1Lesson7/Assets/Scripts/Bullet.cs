using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public int speed=4;
    
    bool right;
    
     public Vector3 position;
    public Vector3 collisionRight;
    public Vector3 collisionLeft;
    public Vector3 collisionV;
    public bool player;
    Transform playerTransform;


    void Start()
    {
        
        if (player)
        {
            right = Player.right;
            collisionRight = new Vector3(1f, 0, 0);
            collisionLeft = new Vector3(-1f, 0, 0);
        }
        else
        {
            playerTransform = GameObject.Find("Player").transform;
            if (transform.position.x < playerTransform.position.x)
            {
                collisionV = new Vector3(3f, 1f, 0);
                right = true;
                
            } else
            {
                collisionV = new Vector3(-3f, 1f, 0);
                right = false;
            }
        }

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
    
    private void OnCollisionEnter2D(Collision2D collision)// Отскок от пули и ее уничтожение
    {
        if (player)
        {
            if (collision.gameObject.tag == "Enimes" )
            {
                GameObject temp = collision.gameObject;
                temp.GetComponent<Enime>().live -= damage;
                if (right) temp.GetComponent<Rigidbody2D>().AddForce(collisionRight, ForceMode2D.Impulse);
                else temp.GetComponent<Rigidbody2D>().AddForce(collisionLeft, ForceMode2D.Impulse);
                Destroy(gameObject);
                if(!collision.gameObject.GetComponent<Enime>()._aS.isPlaying)
                 collision.gameObject.GetComponent<Enime>()._aS.Play();

            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObject temp = collision.gameObject;
                temp.GetComponent<Player>().live -= damage;
                temp.GetComponent<Rigidbody2D>().AddForce(collisionV, ForceMode2D.Impulse);                
                Destroy(gameObject);
                AudioSource a = playerTransform.GetComponent<Player>()._aS;
                a.clip=playerTransform.GetComponent<Player>()._amClip[1];
                a.Play();
               
            }
        }
    }
    
    private void FixedUpdate()
    {
        //Самоуничтожиться
       // Destroy(GetComponent<Bullet>(), 2);
    }
}
