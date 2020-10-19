using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScore : MonoBehaviour
{
    public Text scoreText;

    //fetch score
    public int score = 0;

    void Start()
    {
        DisplayScoreText(score);
    }

    void Update()
    {
        DisplayScoreText(score);
    }

    private void DisplayScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
