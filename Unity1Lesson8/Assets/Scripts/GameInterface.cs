using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public Player _cPlayer;
    public Slider _cSlider;
    public GameObject[] _gmLivs;

    void Start()
    {
        
       
        _cPlayer = FindObjectOfType<Player>();
        _cSlider = GetComponentInChildren<Slider>();
        _cSlider.value = _cPlayer.live;
        //_gmLivs = new GameObject[3];
        _gmLivs = GameObject.FindGameObjectsWithTag("livs");
         

    }


    void FixedUpdate()
    {
        _cSlider.value = _cPlayer.live;
        for(int i = 0; i < _gmLivs.Length; i++)
        {
            if (i < _cPlayer.livs) _gmLivs[i].SetActive(true);
            else _gmLivs[i].SetActive(false);
        }
    }
}
