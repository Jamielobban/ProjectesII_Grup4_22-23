using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUpTimer : MonoBehaviour
{
    public Slider timerBar;
    public Text ammoDisplay;

    public void SetTime(float time)
    {
        timerBar.value = time;
        //if(timerBar.value == timerBar.maxValue)
        //{

        //}
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        

    }
    public void SetMaxTime(float time)
    {
        timerBar.maxValue = time;
        timerBar.value = time;
    }

    public void SetNormalMaxTime(float time)
    {
        timerBar.maxValue = time;
    }

    public float GetMaxTime()
    {
        return timerBar.maxValue;
    }

    public bool CheckValue()
    {
        return timerBar.value == timerBar.maxValue;
    }

    //public float SetBulletsInMagazine() {
        
    //}
}

