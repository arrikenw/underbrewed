﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameText : MonoBehaviour
{
    public GameObject gameTextObject;

    public float waitingTime;

    //public bool countdownComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startText(waitingTime));
    }

    IEnumerator startText(float waitingTime)
    {
        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Ready");
        gameTextObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSecondsRealtime(waitingTime);
        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Go");
        yield return new WaitForSecondsRealtime(waitingTime);
        gameTextObject.GetComponent<Image>().sprite = null;
        gameTextObject.GetComponent<Image>().color = new Color (0, 0, 0, 0);

        ///countdownComplete = true;
    }

    IEnumerator endText()
    {
        gameTextObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Times_Up");
        gameTextObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        yield return null;
    }
}