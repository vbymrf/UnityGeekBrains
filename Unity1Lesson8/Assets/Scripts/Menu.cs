using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{   //Меню
    public GameObject _giStartMenu;
    public GameObject _giEditMenu;
    //Текстовые 
    public GameObject _giHello;
    public GameObject _giStart;
    public GameObject _giBackOnGame;
    public GameObject _giSetting;
    public GameObject _giExit;
    public GameObject _giBack;
    public GameObject _giLanguage;
    public GameObject _giNamePlayer;
    public GameObject _giEnglish;
    public GameObject _giRussian;

    public GameObject _giInputField;
    //Время
    public float timeScale;
    public float fixedDeltaTime;
    

    private void Start()
    {
        if (PlayerPrefs.HasKey("NamePlayer"))
        {
            print("zashli");
            PlayerPrefs.SetString("NamePlayer", "Player");//Записываем имя игрока
            PlayerPrefs.Save();
        }
        
        _giStartMenu = transform.GetChild(0).gameObject;
        _giEditMenu = transform.GetChild(1).gameObject;

        /* _giHello = transform.Find("tHello").gameObject;
        _giStart= transform.Find("tStart").gameObject;
        _giBackOnGame = transform.Find("tBackOnGame").gameObject;
        _giSetting = transform.Find("tSetting").gameObject;
        _giExit = transform.Find("tExit").gameObject;
        _giBack = transform.Find("tBack").gameObject;
        _giLanguadge = transform.Find("tLanguage").gameObject;
        _giNamePlayer = transform.Find("tNamePlayer").gameObject;
        _giEnglish = transform.Find("tEnglish").gameObject;
        _giRussian = transform.Find("tRussian").gameObject;
        _giInputField= transform.Find("InputFild").gameObject;*/
        Poisk(transform, "tHello", ref _giHello);
        Poisk(transform, "tStart", ref _giStart);
        Poisk(transform, "tBackOnGame", ref _giBackOnGame);
        Poisk(transform, "tSetting", ref _giSetting);
        Poisk(transform, "tExit", ref _giExit);
        Poisk(transform, "tBack", ref _giBack);
        Poisk(transform, "tLanguage", ref _giLanguage);
        Poisk(transform, "tNamePlayer", ref _giNamePlayer);
        Poisk(transform, "tEnglish", ref _giEnglish);
        Poisk(transform, "tRussian", ref _giRussian);
        Poisk(transform, "InputField", ref _giInputField);



        LanguageChangeText();
        

        DontDestroyOnLoad(this.gameObject);
        

        timeScale = Time.timeScale ;
        fixedDeltaTime = Time.fixedDeltaTime;
        Debug.Log("Маштаб времени = " + timeScale + ";  время между упдайтами = " + fixedDeltaTime);
        StartMenu();

        

    }

    public Transform RecrFind(Transform tr, string Name)
    {
        print("Зашли в метод");
        if (tr.childCount == 0)
        {

            print("childCount="+tr.childCount);
            return null;
        }
        print("Перед циклом");
        for( int i=0;i< tr.childCount; i++)
        {
            print("Внутри цикла childCount=" + tr.childCount);

            if (tr.GetChild(i).gameObject.name == Name)
            {
                print("Нашли обьект");
                return tr.GetChild(i);                
            }
            print("Не нашли обьект");
            RecrFind(tr.GetChild(i), Name);
            //print("После рeкрутинга");
            
           
        }
        print("После цикла");
        return null;        
    }

    public void Poisk(Transform tr, string Name,ref GameObject   exit)
    {
        //
       // print("Зашли в метод");
        if (tr.childCount == 0)
        {
           // print("childCount=" + tr.childCount);
            return;
        }
        //print("Перед циклом");
        for (int i = 0; i < tr.childCount; i++)
        {
            
          //  print("Внутри цикла childCount=" + tr.childCount);

            if (tr.GetChild(i).gameObject.name == Name)
            {
               // print("Нашли обьект");
                exit= tr.GetChild(i).gameObject;
                return;
            }
          //  print("Не нашли обьект");
            Poisk(tr.GetChild(i), Name, ref exit);
            if (tr.GetChild(i).gameObject.name == Name) return;
         //   print("После рeкрутинга");
        }
      //  print("После цикла");
        //exit = null;
    }



    public void Exit()
    {
        //GameObject t = new GameObject();
        //RecrFind2(transform, "Setting", ref t);
        //Debug.Log(t.name);

        //Debug.Log(RecrFind(transform, "Setting").name);

        // _giHello = .gameObject;

        Application.Quit();
    }
    public void StartGame()
    {
        _giStartMenu.SetActive(false);
        _giEditMenu.SetActive(false);
        SceneManager.LoadScene(1);
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = fixedDeltaTime;
    }
    public void BackOnGame()
    {
        _giStartMenu.SetActive(false);
        _giEditMenu.SetActive(false);
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = fixedDeltaTime;
    }

    public void EditMenu()
    {
        _giStartMenu.SetActive(false);
        _giEditMenu.SetActive(true);
    }
    public void StartMenu()
    {
        _giStartMenu.SetActive(true);
        _giEditMenu.SetActive(false);
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }
    public void EnglishCheckBox()
    {
        Language.L.SetLanguadge("English");
        LanguageChangeText();
    }
    public void RussianCheckBox()
    {
        Language.L.SetLanguadge("Russian");
        LanguageChangeText();
    }
    public void LanguageChangeText()
    {
       
        _giHello.GetComponent<Text>().text =Language.L.iHello+" " + PlayerPrefs.GetString("NamePlayer") + "!";
        _giStart.GetComponent<Text>().text = Language.L.iStart;
        _giBackOnGame.GetComponent<Text>().text = Language.L.iBackOnGame;
        _giSetting.GetComponent<Text>().text = Language.L.iSetting;
        _giExit.GetComponent<Text>().text = Language.L.iExit;
        _giBack.GetComponent<Text>().text = Language.L.iBack;
        _giLanguage.GetComponent<Text>().text = Language.L.iLanguage;
        _giNamePlayer.GetComponent<Text>().text = Language.L.iNamePlayer;
        _giEnglish.GetComponent<Text>().text = Language.L.iEnglish;
        _giRussian.GetComponent<Text>().text = Language.L.iRussian;
    }
    public void EditName()
    {
        string pn = _giInputField.GetComponent<InputField>().text;
        //Debug.Log(pn);
        PlayerPrefs.SetString("NamePlayer", pn);
        PlayerPrefs.Save();
        _giHello.GetComponent<Text>().text = Language.L.iHello + " " + PlayerPrefs.GetString("NamePlayer") + "!";
        
    }
}
