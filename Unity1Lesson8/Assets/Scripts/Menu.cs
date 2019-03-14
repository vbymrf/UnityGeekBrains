using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas _cStartMenu;
    public Canvas _cEditMenu;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _cStartMenu = transform.GetChild(0).gameObject.GetComponent<Canvas>();
        _cEditMenu = transform.GetChild(1).gameObject.GetComponent<Canvas>();

    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        _cStartMenu.enabled=false;
        _cEditMenu.enabled=false;
        SceneManager.LoadScene(0);
    }
    public void BackOnGame()
    {
        _cStartMenu.enabled = false;
        _cEditMenu.enabled = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 1;
    }

    public void EditInterface()
    {
        _cStartMenu.enabled = false;
        _cEditMenu.enabled = true;
    }
    public void StartInterface()
    {
        _cStartMenu.enabled = true;
        _cEditMenu.enabled = false;
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }
}
