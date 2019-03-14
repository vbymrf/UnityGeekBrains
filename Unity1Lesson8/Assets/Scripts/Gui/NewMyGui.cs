using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMyGui : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Box(new Rect(100, 0, 150, 30), "Привет, моя коробка");
    }
}
