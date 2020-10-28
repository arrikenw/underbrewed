using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderController : MonoBehaviour
{
    public int score; // point value of order

    public bool isFlashing = false;

    public bool timerLow = false;


    public Color warningColor = Color.red;

    public float flashDelay = 0.8f; // lower -> faster 

    private GameObject overlay;

    // Start is called before the first frame update
    void Start()
    {
        GameObject overlay = transform.Find("Overlay").gameObject;
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
