using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    
    Vector3 scaleFlipR;
    Vector3 scaleFlipL;
    Vector3 polet;
    public float TimeBoom = 3;
    public float _force = 1;
    WaitForSeconds timeBoom;
    WaitForSeconds waitFlip = new WaitForSeconds(0.3f);
    List<GameObject> objectAddForce;

    void Start()
    {
        timeBoom = new WaitForSeconds(TimeBoom);
        scaleFlipR = new Vector3(1, 1, 1);
        scaleFlipL = new Vector3(-1, 1, 1);
        StartCoroutine(Flip());//Вращение бомбы
        StartCoroutine(Boom(timeBoom));//Взрыв
    }
    IEnumerator Flip()
    {
        while (true)
        {
            if (transform.localScale == scaleFlipR) transform.localScale = scaleFlipL;
            else transform.localScale = scaleFlipR;
            yield return waitFlip;
        }       
    }
    IEnumerator Boom(WaitForSeconds timeBoom)
    {
        yield return timeBoom;

        foreach(GameObject o in objectAddForce)
        {
            float x= o.transform.position.x-transform.position.x;
            float y= o.transform.position.y - transform.position.y;

            if (o.tag=="Player") o.GetComponent<Player>().live -= (int)Mathf.Round(4 - Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
            else if((o.tag == "Enimes")) o.GetComponent<Enime>().live-= (int)Mathf.Round( 4-Mathf.Sqrt( Mathf.Pow(x,2)+Mathf.Pow(y,2) ) );

            if (x < 0.5) x = 0.5f;
            if (y < 0.5) y = 0.5f;
            polet = new Vector3(_force / x, _force / y, 0);
            o.GetComponent<Rigidbody2D>().AddForce(polet, ForceMode2D.Impulse);
            
        }
        Destroy(gameObject);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if( collision.tag=="Enimes" || collision.tag == "Player")
        {
            Debug.Log("Вошел в тригер" + collision.gameObject);
            objectAddForce.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enimes" || collision.tag == "Player")
        {
            Debug.Log("Вышел из тригер" + collision.name);
            objectAddForce.Remove(collision.gameObject);
        }


    }
    void FixedUpdate()
    {
        if (transform.position.y < -10) Destroy(gameObject);
        
    }
}
