﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class myGUI : MonoBehaviour
{
    private string message;

    public Texture2D icon;    


    void OnGUI()
    {
        // отрисовка панели с текстом
        GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 70, 200, 140), "Main Menu"); 

        // Отрисовка кнопок с выводом сообщений
        if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 35, 180, 30), "Open"))
            message = "Open";
        if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 0, 180, 30), "Save"))
            message = "Save";
        if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 35, 180, 30), "Load"))
            message = "Load";

        GUI.Label(new Rect(220, 10, 100, 30), message);

      
        //Угловые панели
        GUI.Box(new Rect(0, 0, 100, 50), "Top-left");
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Top-right");
        GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom-left");
        GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom-right");


        // Иконка в панели
        GUI.Box(new Rect(10, Screen.height / 2 - 125, 100, 100), icon);

        // Иконка в панели с текстом
        GUI.Box(new Rect(10, Screen.height / 2, 100, 100), new GUIContent("Text", icon));
        

        // Группа GUI
       GUI.BeginGroup(new Rect(Screen.width / 2 - 25, 10, 200, 200));
        GUI.Label(new Rect(0, 0, 50, 20), "Hello");
        GUI.Label(new Rect(0, 25, 50, 20), "World!");
       GUI.EndGroup();
        
    }
}
