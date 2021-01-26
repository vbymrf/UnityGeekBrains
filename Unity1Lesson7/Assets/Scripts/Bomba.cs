using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    
    Vector3 scaleFlipR;
    Vector3 scaleFlipL;
    Vector3 polet;
    public float TimeBoom = 3;
    public float _force = 6;
    WaitForSeconds timeBoom;
    WaitForSeconds waitFlip = new WaitForSeconds(0.3f);
    List<GameObject> objectAddForce;

    void Start()
    {
        timeBoom = new WaitForSeconds(TimeBoom);
        objectAddForce = new List<GameObject>();
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
            if (o != null)
            {
                float x =  o.transform.position.x-transform.position.x;
                float y =  o.transform.position.y-transform.position.y;
                float sila = (5 - Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)))/5;
                //Debug.Log(sila);
                //print("x=" + x + " , y=" + y + "  live=" + (int)Mathf.Round(5 - Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2))));
                if (o.tag == "Player")
                {
                    o.GetComponent<Player>().live -= (int)Mathf.Round(5 - Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
                    if (o.GetComponent<Player>()._aS.clip != null)
                    {
                        AudioSource a = o.GetComponent<Player>()._aS;
                        a.clip = o.GetComponent<Player>()._amClip[0];
                        a.Play();
                    }
                }
                else if (o.tag == "Enimes")
                {
                    o.GetComponent<Enime>().live -= (int)Mathf.Round(5 - Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
                    
                    if (o.GetComponent<AudioSource>()!=null)      
                        if (o.GetComponent<Enime>()._aS.clip != null)
                            if (!o.GetComponent<Enime>()._aS.isPlaying)
                                o.GetComponent<Enime>()._aS.Play();
                    
                }

                
                float modX = 1, modY=1;
                if (x < 0)
                {
                    modX = -1;
                    x *= -1;
                }
                if (y < 0)
                {
                    modY = -1;
                    y *= -1;
                }
                x = modX*_force * sila *(x / (x + y));
                y = modY*_force * sila* (y / (x + y));
                polet = new Vector3(x, y, 0);
                
                //print("o=" + o.transform.position.x + " bomba=" + transform.position.x);
                //print("Вектор отлета "+polet);
                o.GetComponent<Rigidbody2D>().AddForce(polet, ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if( collision.tag=="Enimes" || collision.tag == "Player")
        {
            if (!objectAddForce.Contains(collision.gameObject))
                {
                //Debug.Log("Вошел в тригер " + collision.gameObject);
                objectAddForce.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enimes" || collision.tag == "Player")
        {
           // Debug.Log("Вышел из тригера " + collision.name);
            objectAddForce.Remove(collision.gameObject);
        }


    }
    void FixedUpdate()
    {
        if (transform.position.y < -10) Destroy(gameObject);
        
    }
}
