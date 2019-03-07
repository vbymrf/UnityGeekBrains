using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eni : MonoBehaviour
{
    public List<Enime> Enimes;
    public int random;
    public int Level;
    public WaitForSeconds waitSecond = new WaitForSeconds(5);
   
    //public Enime prefabEnime;
    void Start()
    {
        if(Level == 2) StartCoroutine( InstantiateTime(Enimes[0]));
        
    }
    void InstantiateTwo()
    {

        Vector3 smesh = new Vector3(Random.Range(0, 25), 0, 0);
        random = Random.Range(0, 2);            
            Instantiate(Enimes[random], transform.position+smesh, Quaternion.identity);
    }
    IEnumerator InstantiateTime(Enime enime)
    {
        while (true)
        {
            Vector3 smesh = new Vector3(Random.Range(0, 30), 0, 0);
            Instantiate(enime, transform.position+ smesh, Quaternion.identity);
            yield return waitSecond;
        }
    }
    
    void Update()
    {
        
        if (GameObject.FindGameObjectsWithTag("Enimes").Length<10  && Level==1)
        {
            InstantiateTwo();
            //Invoke("InstantiateTwo", 5);
        }
        
    }
}
