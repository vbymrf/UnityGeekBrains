using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 2;
    public int speed=4;
    public Vector3 smeshen;
    Vector3 nowPositionR;
    Vector3 nowPositionL;
    
    public GameObject player;

   
    void Start()
    {
       // player = GameObject.Find("Player");
        
        nowPositionR = new Vector3(0.592f, -0.028f, 0);
        nowPositionL = new Vector3(-0.592f, -0.028f, 0);
        smeshen = new Vector3(1, 0.5f, 0);
        
    }

    void Move()
    {
//transform.position = player.transform.position+nowPositionR;
        //if (Player.right)
        //{
            
        //} else
        //{
        //    transform.position = player.transform.position + nowPositionL;
        //}
        transform.position = Vector3.MoveTowards(transform.position, transform.position + smeshen, speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
        
    }
}
