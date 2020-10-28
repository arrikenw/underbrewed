using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrderController : MonoBehaviour
{

    public int score; // point value of order

    public bool isFlashing = false;

    public bool timerLow = false;


    public Color warningColor = color.Red;

    public float flashDelay = 0.8f; // lower -> faster 

    // Start is called before the first frame update
    void Start()
    {
        GameObject overlay = this.GameObject().transform.Find("Overlay");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing == true)
        {
            StartCoroutine(FlashingOverlay(overlay, warningColor, flashDelay));
        }
    }

    IEnumerator FlashingOverlay(GameObject overlay, Color warningColor, float flashDelay)
    {
        while (true)
        {
            overlay.GetComponent<RawImage>().color = warningColor;

            yield return new WaitForSeconds(flashDelay);

            overlay.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

            yield return new WaitForSeconds(flashDelay);
        }
    }
}
