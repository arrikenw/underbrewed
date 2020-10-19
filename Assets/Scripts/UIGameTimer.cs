using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTimer : MonoBehaviour
{

    public float timeRemaining = 10;

    //private bool timerEnabled = false;

    public Text timeText;

    void Start()
    {
        DisplayTime(timeRemaining);
        //timerEnabled = true;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            //End of game timer. End game, etc. 
            timeRemaining = 0;
            //timerEnabled = false;
        }

        DisplayTime(timeRemaining);
        
    }

    void DisplayTime(float timeInSeconds)
    {

        timeInSeconds += 1;

        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
