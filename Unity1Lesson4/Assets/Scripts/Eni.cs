using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eni : MonoBehaviour
{
    public List<Enime> Enimes;
    public int random;
   
    //public Enime prefabEnime;
    void Start()
    {
       
        
    }
    void InstantiateEnime()
    {
            transform.position = new Vector3(Random.Range(-8, 7),5 , 0);
            random = Random.Range(0, 2);            
            Instantiate(Enimes[random], transform.position, Quaternion.identity);
    }
    
    void Update()
    {
        
        if (GameObject.FindWithTag("Enimes") == null)
        {
            InstantiateEnime();
            Invoke("InstantiateEnime", 5);
        }
    }
}
