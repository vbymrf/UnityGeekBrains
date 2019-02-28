using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    GameObject Run;
    GameObject Stay;
    //public bool lerp = false;
    Rigidbody2D _rigibody;
    Transform polTransform;
     public Collider2D[] polCollider;
    bool ground=false;
   
      
    public int speed=4;  //скорость перемещения
    public int speedRun = 10;
    //public int vectorRun = 5;
    public int live=10;// Жизнь песонажа
    public float AxisX;
    Vector3 smeshen;  
    Vector3 scaleRight;
    Vector3 scaleLeft;    
    Vector3 run;
    #region Пуля
    
    public static bool right { get; set; } = true; // Положение снаряда
    public GameObject prefBullet; //Связь с обьектом пули
    Vector3 nowPositionR;
    Vector3 nowPositionL;
    GameObject temp;
    #endregion
    

    void Start()
    {
        Stay = transform.GetChild(0).gameObject;
        Run = transform.GetChild(1).gameObject;
        Run.SetActive(false);
        smeshen = new Vector3(0, 0, 0);
        scaleRight = new Vector3(1, 1, 1);
        scaleLeft= new Vector3(-1, 1, 1);
        run = new Vector3(0, 1, 0);
       

        polTransform = transform.GetChild(3);
// Задаем вектора для появления пули
        nowPositionR = new Vector3(0.6f, 0, 0);
        nowPositionL = new Vector3(-0.6f, 0, 0);
        _rigibody = GetComponent<Rigidbody2D>();
    }
    private void Move() // Перемещение персонажа
    {

        smeshen.x = AxisX;
        if (AxisX > 0)
        {
            transform.localScale = scaleRight;
            right = true;
        }
        else
        {
            transform.localScale = scaleLeft;
            right = false;
        }
        //transform.position= Vector3.Lerp(transform.position, transform.position+smeshen, speed*Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + smeshen, speed * Time.deltaTime);
    }
    void Bul() // Выстрел персонажа
    {
        if (right)
        {
            temp = Instantiate(prefBullet, transform.position + nowPositionR, Quaternion.identity);
            print("Выстрел правый");
        }
        else
            temp = Instantiate(prefBullet, transform.position + nowPositionL, Quaternion.identity);
            Destroy(temp, 5);
    }
    void RunPlayer()//прыжок персонажа
    {
        //if(lerp)
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + run, Time.deltaTime*speedRun*10);
        //else
        //transform.position = Vector3.Lerp(transform.position, transform.position + run, Time.deltaTime * speedRun);
        if(ground)
        _rigibody.AddForce(run*speedRun,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    void Update()
    {
        AxisX=Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            Stay.SetActive(false);
            Run.SetActive(true);
            Move();
        }
        else
        {
            Stay.SetActive(true);
            Run.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)  || Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            Bul();
            print("Выстрел");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RunPlayer();
        } 
       

        if (transform.position.y < -10 || live <= 0) SceneManager.LoadScene(0);
    }
    private void FixedUpdate()
    {
        polCollider = Physics2D.OverlapCircleAll(polTransform.position,1);
        if (polCollider.Length > 1) ground = true;
        else ground = false;
    }
}
