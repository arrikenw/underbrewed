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
            titleText.text = "YOU PASSED!";
        }
        else
        {
            titleText.text = "YOU FAILED!";
        }
    }

    public void updateGameStatistics(float percentage, int score, int highScore)
    {
        string grade = "grade"; 

        if (percentage < 50)
        {
            grade = "N";
        } else if (percentage >= 50 && percentage < 65)
        {
            grade = "P";
        } else if (percentage >= 65 && percentage < 70)
        {
            grade = "H3";
        } else if (percentage >= 70 && percentage < 75)
        {
            grade = "H2B";
        } else if (percentage >= 75 && percentage < 80)
        {
            grade = "H2A";
        } else if (percentage >= 80)
        {
            grade = "H1";
        }

        this.gameObject.transform.Find("GameStatistics").GetComponent<Text>().text = string.Format("Score: {0}\nGrade: {1}\n\nHigh score: {2}", score, grade, highScore); 
    }
    
}
