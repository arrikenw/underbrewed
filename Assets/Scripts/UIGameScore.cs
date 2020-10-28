using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScore : MonoBehaviour
{
    public void updateGameScore(int newScore)
        {
            this.gameObject.GetComponent<Text>().text = newScore.ToString();
        }
}

