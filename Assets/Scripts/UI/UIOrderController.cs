using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderController : MonoBehaviour
{
    public int score; // value of order

    public Color warningColor = Color.red;

    public float flashDelay = 0.8f; // time between flashes

    public bool isFlashing = false; // check if timer is already flashing

    public UIOrderTimer timer;

    bool timerLow = false; // check if timer is low

    void Awake()
    {
        timer = transform.Find("OrderTimer").GetComponent<UIOrderTimer>();
    }

    void Update()
    {
        timerLow = timer.timerLow;

        if (timerLow == true && isFlashing == false)
        {
            StartCoroutine(flashing(warningColor, flashDelay));
            isFlashing = true;
        }
    }

    public void updateOrderTimer(float newTime)
    {
        timer.timeRemaining = newTime;
    }

    IEnumerator flashing(Color warningColor, float flashDelay)
    {
        while (true)
        {
            this.gameObject.GetComponent<Image>().color = warningColor;

            yield return new WaitForSeconds(flashDelay);

            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            yield return new WaitForSeconds(flashDelay);
        }
    }
}
