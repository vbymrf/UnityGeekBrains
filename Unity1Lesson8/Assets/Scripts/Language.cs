using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
    private Language() { }
    public static readonly Language L = new Language();

    public string language = "English";

    public string iHello = "Hello";
    public string iStart = "Start";
    public string iBackOnGame = "Back on Game";
    public string iSetting = "Setting";
    public string iExit = "Exit";
    public string iBack = "Back";
    public string iLanguage = "Language";
    public string iNamePlayer = "Name player";


    public string iEnglish = "English";
    public string iRussian = "Русский";

    public void SetLanguadge(string language)
    {
        this.language = language;
        if (language == "Russian")
        {
 iHello = "Привет игрок";
   iStart = "Старт";
     iBackOnGame = "Вернуться в игру";
     iSetting = "Настройки";
     iExit = "Выход";
 iBack = "Назад";
 iLanguage = "Язык интерфейса  ";
 iNamePlayer = "Имя игрока  ";
}
        else if (language == "English")
        {
iHello = "Hello";
 iStart = "Start";
 iBackOnGame = "Back on Game";
    iSetting = "Setting";
   iExit = "Exit";
     iBack = "Back";
  iLanguage = "Setting";
  iNamePlayer = "Name player";
}
}

        
}
     
     

