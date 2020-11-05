using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressManager : MonoBehaviour
{
    public GameObject progressUI;

    void Update()
    {
        foreach (Transform child in progressUI.transform)
        {
            float maxTime = child.gameObject.GetComponent<UIProgressBar>().maxTime;
            GameObject station = child.gameObject.GetComponent<UIProgressBar>().station;
            float timeLeft = 0.0f;

            //check value of station
            if (station.GetComponent<ToggleProcessor>())
            {
                timeLeft = station.GetComponent<ToggleProcessor>().timeUntilComplete;
            } else if (station.GetComponent<HoldProcessor>())
            {
                if (station.GetComponent<HoldProcessor>().interacting == false)
                {
                    timeLeft = 0.0f;
                }
                else {
                    timeLeft = station.GetComponent<HoldProcessor>().timeUntilComplete;
                }
            }

            if (timeLeft > 0 && timeLeft < maxTime)
            {
                child.gameObject.SetActive(true);
                child.gameObject.GetComponent<Slider>().value = ((maxTime - timeLeft) / maxTime);
            }
            else
            {
                child.gameObject.SetActive(false); 
            }
        }
    }


}
