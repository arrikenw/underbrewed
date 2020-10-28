using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScore : MonoBehaviour
{
    public void updateGameScore(int newScore)
        {
            scoreText.text = score.ToString();
        }
}

