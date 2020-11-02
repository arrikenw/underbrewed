using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameStatistics : MonoBehaviour
{
    //public GameObject textObject;

    public void updateGameStatistics(int completed, int failed, int score, int highScore)
    {
        this.gameObject.GetComponent<Text>().text = string.Format("Orders completed: {0}\nOrders failed: {1}\nTotal score: {2}\nCurrent high score: {3}", completed, failed, score, highScore); 
    }
    
}
