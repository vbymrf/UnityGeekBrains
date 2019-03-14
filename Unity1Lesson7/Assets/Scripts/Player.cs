﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   // public static GameObject thisGameObject;
    
    
    GameObject Stay;
    //public bool lerp = false;
    Rigidbody2D _rigibody;
    public Transform polTransform;
    //public Collider2D[] polCollider;
    public RaycastHit2D[] racasti;
    bool ground=false;
    public float radiusPola=0.51f;
    public Coroutine _coroutina;
    Color exitCoroutina;
    Material _colorChange1;
    Material _colorChange2;
    public Color _color;
    Color _colorWhite;
    public float timeColor=0.1f;
    public WaitForSeconds waitColor = new WaitForSeconds(0.01f);
      
    public int speed=4;  //скорость перемещения
    public int speedRun = 10;
    //public int vectorRun = 5;
    public int live=10;// Здоровье персонажа
    public int livs = 3;// Жизнь персонажа
    RaycastHit2D[] _rLiveNewPosit;


    public float AxisX;
    Vector3 smeshen;  
    Vector3 scaleRight;
    Vector3 scaleLeft;    
    Vector3 run;
    Vector2 collisionEnimeRight;
    Vector2 collisionEnimeLeft;
    Vector2 velA;

    Rigidbody2D _cRigi;
    //Vector3 _vRelativeVelosity;
    float Xv=0;
    float Yv=0;
    Animator _cAnimator;
    float Ypred;
    

    public AudioSource _aS;
    public AudioClip[] _amClip;

    #region Пуля

    public static bool right { get; set; } = true; // Положение снаряда
    public GameObject prefBullet; //Связь с обьектом пули
    Vector3 nowPositionR;
    Vector3 nowPositionL;
    GameObject temp;
    #endregion
    public GameObject prefabBomba;
    private void OnGUI()
    {
        
        //Vector3 vect = Camera.main.WorldToScreenPoint(transform.position);
        //Rect liveRect = new Rect(vect.x + 10, Screen.height - vect.y - 30, 20, 20);
        //GUI.Box(liveRect, live.ToString());
        #region Отладка
        //GUILayout.Label("Velocity X="+Xv.ToString()+" Y="+ Yv.ToString());
        //GUILayout.Toggle(ground,"ground");    
        #endregion
    }
    
    private void OnCollisionStay(Collision collision)
    {
       // _vRelativeVelosity=collision.relativeVelocity;
    }
    void Start()
    {
       _aS = GetComponent<AudioSource>();
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //DontDestroyOnLoad(this.gameObject);
        _cAnimator = GetComponent<Animator>();
        _cRigi =  GetComponent<Rigidbody2D>();
        Stay = transform.GetChild(0).gameObject;
        Stay.SetActive(false);
        
        smeshen = new Vector3(0, 0, 0);
        scaleRight = new Vector3(1, 1, 1);
        scaleLeft= new Vector3(-1, 1, 1);
        run = new Vector3(0, 1, 0);
        collisionEnimeRight=new Vector2(3,3);
        collisionEnimeLeft = new Vector2(-3, 3);

        

        polTransform = transform.GetChild(2);
// Задаем вектора для появления пули
        nowPositionR = new Vector3(0.6f, 0, 0);
        nowPositionL = new Vector3(-0.6f, 0, 0);
        _rigibody = GetComponent<Rigidbody2D>();

        // Корутина
        timeColor = 0.1f;// Скорость изменения цвета
        _colorChange1 = GetComponent<SpriteRenderer>().material;
        
        _colorWhite = _colorChange1.color; 
 
    }

    

    IEnumerator ChangeColor(Color color)
    {
        
        
        while (_colorChange1.color != color)
        {
            _colorChange1.color = Color.Lerp(_colorChange1.color, color, timeColor);
           
            //if (exitCoroutina) {
            //    yield break;
            //}
            yield return waitColor;
        }
        
        while (_colorChange1.color != _colorWhite)
        {
            _colorChange1.color = Color.Lerp(_colorChange1.color, _colorWhite, timeColor);
          
            //if (exitCoroutina)
            //{
            //    yield break;
            //}
            yield return waitColor;
        }
        
    }
    private void Move() // Перемещение персонажа
    {

        //smeshen.x = AxisX;
        if (AxisX > 0)
        {
            transform.localScale = scaleRight;
            right = true;
            smeshen.x = 1;
        }
        else
        {
            transform.localScale = scaleLeft;
            right = false;
            smeshen.x = -1;
        }
        //transform.position= Vector3.Lerp(transform.position, transform.position+smeshen, speed*Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + smeshen, speed * Time.deltaTime);
    }
    void Bul() // Выстрел персонажа
    {
        if (right)
        {
            temp = Instantiate(prefBullet, transform.position + nowPositionR, Quaternion.identity);
            //print("Выстрел правый");
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
//

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enimes")
        {
            if (transform.position.x > collision.transform.position.x) GetComponent<Rigidbody2D>().AddForce(collisionEnimeRight, ForceMode2D.Impulse);
            else GetComponent<Rigidbody2D>().AddForce(collisionEnimeLeft, ForceMode2D.Impulse);
            live--;
            if(_coroutina!=null) StopCoroutine(_coroutina);            
            _coroutina = StartCoroutine(ChangeColor(_color));
 _aS.clip = _amClip[0];
                _aS.Play();
            if (!_aS.isPlaying)
            {
               
            }
        } 
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    void Update()
    {
        #region Управление героем

        AxisX = Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            // Stay.SetActive(false);

            Move();
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            Bul();
            //print("Выстрел");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RunPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(prefabBomba, transform.position, Quaternion.identity);
        }
        #endregion

        #region Потеря жизни
        if (transform.position.y < -10 || live <= 0)
        {
            live = 10;
            livs--;
            //Поиск площадки для зарождения
            float X1, Y1, a=1,c=1;
            X1 = transform.position.x;
            Y1 = -10;
            
            bool transformPlayer = true;
            while (transformPlayer)
            {
                _rLiveNewPosit = Physics2D.RaycastAll(new Vector2(X1, Y1), new Vector2(0, 100));
                foreach (RaycastHit2D r in _rLiveNewPosit)
                {
                
                if (r.transform.tag == "Platform")
                    {
                      
                        transform.position = new Vector3(r.transform.position.x, r.transform.position.y + 5, r.transform.position.z);
                        //transformPlayer = false;
                        return;
                        //break;
                        
                    }
                }
                //Вращение луча для обнаружения платформы
                X1 = X1 + a * c;
                a = a + 0.5f;
                c = c * -1;
            }
         }
        if (livs <= 0)
        {
            SceneManager.LoadScene(0);
        }
        #endregion


        #region Анимация персонажа

        if (ground && AxisX==0)//Если на земле стоим
        {
            _cAnimator.SetBool("Run", false);
            _cAnimator.SetBool("jumpDown", false);
            _cAnimator.SetBool("Idle", true);
            _cAnimator.SetBool("jumpUp", false);
        } else if(ground && AxisX != 0) //Если на земле движимся
        {
            _cAnimator.SetBool("Run", true);
            _cAnimator.SetBool("jumpDown", false);
            
        }
        if (Yv > 0.3)//Если прыгаем
        {
            _cAnimator.SetBool("jumpUp", true);
            _cAnimator.SetBool("Idle", false);
        } else if (Yv < 0 && Ypred>transform.position.y)//Если падаем
        {
            _cAnimator.SetBool("jumpDown", true);
            _cAnimator.SetBool("Idle", false);
            _cAnimator.SetBool("jumpUp", false);

        }
        #endregion
        //if(Ypred == transform.position.y)
        //{
        //    print("сошлись");
        //}
    }
    private void FixedUpdate()
    {
        Ypred = transform.position.y;
        //polCollider = Physics2D.OverlapCircleAll(polTransform.position,radiusPola);

        //if (polCollider.Length > 1) ground = true;
        //else ground = false;
        racasti = Physics2D.LinecastAll(transform.position, transform.position + (new Vector3(0, -radiusPola, 0)));
        //Debug.Log(racasti[1]);

        if (racasti.Length > 1) ground = true;
        else ground = false;
        Xv=Okr(_cRigi.velocity.x,5);
        Yv = Okr(_cRigi.velocity.y,5); 
    }
    float Okr(float f, float n)
    {
        f = f * Mathf.Pow(10, n);
        f = Mathf.Round(f);        
        f =f* Mathf.Pow(10,-n); 
        return f;
    }
}
