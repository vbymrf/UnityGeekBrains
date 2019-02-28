using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eni : MonoBehaviour
{
    public List<Enime> Enimes;
    public Enime prefabEnime;
    void Start()
    {
       
        
    }

    
    void Update()
    {
        print(GameObject.FindWithTag("Enimes"));
        if (GameObject.FindWithTag("Enimes") == null)
        {
            transform.position = new Vector3(Random.Range(-8, 7),5 , 0);
            Instantiate(prefabEnime, transform.position, Quaternion.identity);
        }
    }
}
