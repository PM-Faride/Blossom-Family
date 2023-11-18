using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
//using UnityEngine.Events;
//using 
public class Timer : MonoBehaviour
{
    //private int minTimer;
    //private int secTimer;
    //private int milTimer = 0;
    private bool activeUpdate = false;
    //private TimeSpan time;
    //public static bool stopTimer;
    private float timer;

    [SerializeField] private TextMeshProUGUI secondsTxt;
    [SerializeField] private TextMeshProUGUI miliSeconds;
    //[SerializeField] private UnityEvent StopTheGame;
    //[SerializeField] private float countDownTime;
    // Start is called before the first frame update
    void Start()
    {
        //Reset();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activeUpdate)
        {
            timer += Time.fixedDeltaTime;
            UpdateTimerDisplay(timer * 1000);
            //if(timer > 0)
            //{
            //}
            //else
            //{
            //    //Flash();
            //    secondsTxt.text = "00";
            //    miliSeconds.text = "00";
            //    StopTheGame.Invoke();
            //}
        }
    }
    public void Reset()
    {
        timer = 0;
        activeUpdate= true;
    }

    private void UpdateTimerDisplay(float time)
    {
        float seconds = Mathf.FloorToInt(time / 1000);
        float msecs = Mathf.FloorToInt(time % 1000);

        string currentTime = string.Format("{00:00}{1:00}", seconds, msecs);
        secondsTxt.text = currentTime[0].ToString() + currentTime[1].ToString();
        miliSeconds.text = currentTime[2].ToString() + currentTime[3].ToString();
    }

    public void StopTimer()
    {
        activeUpdate = false;
    }

    public void Zero()
    {
        secondsTxt.text = "00";
        miliSeconds.text = "00";
    }
    //public void StopTimer(bool zero)
    //{
    //    activeUpdate = false;
    //    //if (zero)
    //    //{

    //    //    secondsTxt.text = "00";
    //    //    miliSeconds.text = "00";
    //    //}
    //}
    //private void Flash()
    //{

    //}
}
