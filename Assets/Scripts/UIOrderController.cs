using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderController : MonoBehaviour
{
    public int score; // value of order


    public Color warningColor = Color.red; 

    public float flashDelay = 0.8f; // time between flashes

    public bool isFlashing = false; // check if already flashing

    public bool timerLow = false; // check if timer is low

    void Start()
    {
        //
    }

    void Update()
    {
        if (timerLow == false && isFlashing == false)
        {
            StartCoroutine(FlashingOverlay(warningColor, flashDelay));
        }
    }

    public void updateOrderTimer(int newTime)
    {
        UIOrderTimer timer = transform.Find("OrderTimer").GetComponent<UIOrderTimer>();
        timer.timeRemaining = newTime;

    }

    IEnumerator FlashingOverlay(Color warningColor, float flashDelay)
    {
        while (true)
        {
            gameObject.GetComponent<RawImage>().color = warningColor;

            yield return new WaitForSeconds(flashDelay);

            gameObject.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

            yield return new WaitForSeconds(flashDelay);
        }
    }
}
