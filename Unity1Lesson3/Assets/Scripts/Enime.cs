using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enime : MonoBehaviour
{
    public int live = 10;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.position.y < -10 || live <= 0) Destroy(gameObject);
    }
}
