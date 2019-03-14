using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enime : MonoBehaviour
{
    public int live = 10;//жизнь
    public bool round;
    Rigidbody2D _rigibody;
    public float silaVrasheniy = 10;
    int impulse ;
    public bool Patrol;
    public bool Visible;
    public bool atack;
    public int speed;// скорость движения
    public bool right=false;// идем на право
    public List<RaycastHit2D> seeObject;
    public float distanceSee;
    public LayerMask seeMask;
    public RaycastHit2D[] vidim;
    GameObject Player;
    public GameObject prefabBullet;
    public float timeWaitBullet=2f;

    public AudioSource _aS;

    bool zadergkaTime=true;
    WaitForSeconds waitBullet;//время между выстрелами


    Vector3 step;
    Vector3 seeUgl;
    Vector3 nol;
    
    Vector3 _scaleRight;
    Vector3 _scaleLeft;
    private bool groundAs;

    void Start()
    {
        _aS = GetComponent<AudioSource>();
      _rigibody = GetComponent<Rigidbody2D>();
        impulse = 0;
        
        nol = new Vector3(0, 0, 0);

        waitBullet = new WaitForSeconds(timeWaitBullet);

        Player = GameObject.FindGameObjectWithTag("Player");

        seeUgl = new Vector3(0, -1, 0);
        if (right)
        {
            
            step = new Vector3(1, 0, 0);
        }

        else
        {
            step = new Vector3(-1, 0, 0);
        }

        _scaleRight = new Vector3(1, 1, 1);
        _scaleLeft = new Vector3(-1, 1, 1);

       

    }
    private void FixedUpdate()
    {
        if (round)
        { if (impulse++ < 5)
            {
                _rigibody.AddTorque(silaVrasheniy, ForceMode2D.Impulse);                
            }            
        }
        else
        {
            if (Visible)
            {
                vidim = Physics2D.RaycastAll(transform.position, step+seeUgl, distanceSee, seeMask);
                //Debug.DrawRay(transform.position, step* distanceSee + seeUgl* distanceSee, Color.red);
                //foreach(RaycastHit2D vid in vidim) Debug.Log(vid.collider.name);
                if (Patrol)
                {
                    seeUgl.y = -1;
                    distanceSee = 3;
                    Move();
                    groundAs = false;
                    foreach (RaycastHit2D vid in vidim)
                    {
                        if (vid.transform.tag == "Platform") groundAs = true;
                    }
                    if (!groundAs)
                    {                        
                        Flip();
                    }

                }
                else
                {
                    seeUgl.y = 0;
                    distanceSee = 3;
                    if (Player.transform.position.y <= transform.position.y+0.5)
                    {
                        if (Player.transform.position.x < transform.position.x)
                        {
                            step.x = -1;
                            Move();
                        }
                        else
                        {
                            step.x = 1;
                            Move();
                        }
                        foreach (RaycastHit2D vid in vidim)
                        {
                            if (vid.transform.tag == "Player") atack = true;
                        }
                        
                        if (atack && zadergkaTime)
                        {
                            Bullet();
                            StartCoroutine(zadergka());
                        }
                    } else
                    {
                       Patrol = true;
                    }
                }
            }
        }
    }
    IEnumerator zadergka()
    {
        zadergkaTime = false;
        yield return waitBullet;
        zadergkaTime = true;
    }

        void Bullet()
    {       
            GameObject temp;
            if (Player.transform.position.x > transform.position.x)
            {
                 temp= Instantiate(prefabBullet, transform.position + new Vector3(0.7f, 0.8f, 0), Quaternion.identity);
            }
            else
            {
                temp = Instantiate(prefabBullet, transform.position + new Vector3(-0.7f, 0.8f, 0), Quaternion.identity);
            }
            Destroy(temp,5);         
    }
   void  Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + step, Time.deltaTime*speed);
    }
    Vector3 Flip()
    {
        right = !right;
        if (right)
        {
            step.x = 1;
            GetComponent<Transform>().localScale = _scaleRight;
        }
        else
        {
            step.x = -1;
            GetComponent<Transform>().localScale = _scaleLeft; 
        }
        return step;
    }
    void Update()
    {
        if (transform.position.y < -10 || live <= 0) Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.transform.tag == "Enimes") Flip(); 
    }        
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log("Внутри находится:"+collider.name);
        if(collider.gameObject.tag=="Player")
        Patrol = false;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //Debug.Log("Вышел:" + collider.gameObject.name);
        if (collider.gameObject.tag == "Player")
            Patrol = true;
    }

    private void OnBecameVisible()
    {
        Visible = true;
    }
    private void OnBecameInvisible()
    {
        Visible = false; 
    }
    
}
