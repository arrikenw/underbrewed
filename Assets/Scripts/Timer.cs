using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider slider;
    public float maxTime; // time allowed for task

    public Color startColor;
    public Color midColor;
    public Color finColor;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private float elapsedTime = 0.0f; // time passed since timer started

    private float fillValue;

    void Start()
    {
        gradient = new Gradient();

        colorKey = new GradientColorKey[3];
        colorKey[0].color  = startColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = midColor;
        colorKey[1].time = 0.5f;
        colorKey[1].color = finColor;
        colorKey[1].time = 1.0f;


        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        fillValue = ((maxTime - elapsedTime) / maxTime);

        slider.value = fillValue;

        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = gradient.Evaluate(fillValue);
    }

    // change value based on progress points 

    // change colour based on value

}
