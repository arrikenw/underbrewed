using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTimer : MonoBehaviour
{
    public void updateGameTimer(int newTime)
    {
        float minutes = Mathf.FloorToInt(newTime / 60);
        float seconds = Mathf.FloorToInt(newTime % 60);

        this.gameObject.transform.Find("Text").GetComponent<Text>().text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
