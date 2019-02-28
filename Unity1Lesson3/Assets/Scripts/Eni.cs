using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eni : MonoBehaviour
{
    public List<Enime> Enimes;
    public Enime prefabEnime;
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
           
        }
        
    }

    
    void Update()
    {
        print(GameObject.FindWithTag("Enimes"));
        if (GameObject.FindWithTag("Enimes") == null) Instantiate(prefabEnime, transform.position, Quaternion.identity);
    }
}
