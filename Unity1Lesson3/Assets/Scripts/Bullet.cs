using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 2;
    public int speed=4;
    
    bool right;
    
     public Vector3 position;
    public GameObject player;

   
    void Start()
    {
       // player = GameObject.Find("Player");
        
        right = Player.right;
        
       
        
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
    private void FixedUpdate()
    {
        //Destroy(gameObject, 2);
    }
}
