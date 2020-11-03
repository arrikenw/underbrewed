using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndScreen : MonoBehaviour
{

    public void updateTitleText(bool win)
    {
        Text titleText = this.gameObject.transform.Find("TitleText").GetComponent<Text>();

        if (win)
        {
            titleText.text = "YOU WON!";
        }
        else
        {
            titleText.text = "YOU LOST!";
        }
    }

    public void updateGameStatistics(float completed, int score, int highScore)
    {
        this.gameObject.transform.Find("GameStatistics").GetComponent<Text>().text = string.Format("% of orders completed: {0}\nRound score: {1}\nHigh score: {2}", completed, score, highScore); 
    }
    
}
