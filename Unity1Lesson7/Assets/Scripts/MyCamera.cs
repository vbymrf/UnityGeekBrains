using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    Vector3 nowPosition;
    Transform Player;
    public bool _lerp=true;
    public int _moveSpeed=10;

    void Start()
    {
        nowPosition = new Vector3(0, 0, -1000f);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Player.position;
    }
   

    private void Update()
    {
        nowPosition.x = Player.position.x;
        nowPosition.y= Player.position.y;
        if(_lerp)
        transform.position = Vector3.Lerp(transform.position, nowPosition, Time.deltaTime);
        else
        transform.position = Vector3.MoveTowards(transform.position, nowPosition, Time.deltaTime * _moveSpeed);
    }
    void FixedUpdate()
    {
        
    }
}
