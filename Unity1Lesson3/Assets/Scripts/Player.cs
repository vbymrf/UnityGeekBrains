using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    GameObject Run;
    GameObject Stay;
    Vector3 smeshen;
    Vector3 scaleRight;
    Vector3 scaleLeft;
    int speed=4;
    public float AxisX;
    public int live=10;
    public static bool right { get; set; } = true;
    public GameObject prefBullet;
    GameObject temp;

    Vector3 nowPositionR;
    Vector3 nowPositionL;

    void Start()
    {
        Stay = transform.GetChild(0).gameObject;
        Run = transform.GetChild(1).gameObject;
        Run.SetActive(false);
        smeshen = new Vector3(0, 0, 0);
        scaleRight = new Vector3(1, 1, 1);
        scaleLeft= new Vector3(-1, 1, 1);
// Задаем вектора для появления пули
        nowPositionR = new Vector3(0.6f, 0, 0);
        nowPositionL = new Vector3(-0.6f, 0, 0);
    }
    private void Move()
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
    void Bul()
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
            
            if(right)
            temp = Instantiate(prefBullet,transform.position+ nowPositionR, Quaternion.identity);
            else
                temp = Instantiate(prefBullet, transform.position + nowPositionL, Quaternion.identity);
            //Time.timeScale = 0;
            //Time.fixedDeltaTime;
             

            Destroy(temp, 5);
        }

        if (transform.position.y < -10 || live <= 0) SceneManager.LoadScene(0);
    }
}
