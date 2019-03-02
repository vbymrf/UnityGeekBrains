using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enime : MonoBehaviour
{
    public int live = 10;
    public bool round;
    Rigidbody2D _rigibody;
    public float sila = 10;
    int impulse ;
    
    void Start()
    {
      _rigibody = GetComponent<Rigidbody2D>();
        impulse = 0;
    }
    private void FixedUpdate()
    {
        if (round)
        { if (impulse++ < 5)
            {
                _rigibody.AddTorque(sila, ForceMode2D.Impulse);                
            }            
        }
        else
        {

        }
    }

    void Update()
    {
        if (transform.position.y < -10 || live <= 0) Destroy(gameObject);
    }
}
