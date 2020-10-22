using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecipeTimer : MonoBehaviour
{
    public Slider timer;
    public float maxTime = 45.0f; // time allowed for task

    public Color lowColor = Color.red;
    public Color midColor = Color.yellow;
    public Color highColor = Color.green;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private float elapsedTime = 0.0f; // time passed since timer started

    public float fillValue;

    public bool timerEnabled = false;

    public bool flashing;

    public GameObject recipe;

    public Color warningColor;

    public float waitingTime;

    private bool isFlashing;

    void Start()
    {
        gradient = new Gradient();

        colorKey = new GradientColorKey[3];
        colorKey[0].color  = lowColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = midColor;
        colorKey[1].time = 0.5f;
        colorKey[2].color = highColor;
        colorKey[2].time = 1.0f;


        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        timerEnabled = true;

        recipe = transform.parent.gameObject;

        isFlashing = false;

        if (flashing == true)
        {
            StartCoroutine(FlashingOverlay(recipe, warningColor, waitingTime));
            isFlashing = true;
        }
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;

        fillValue = ((maxTime - elapsedTime) / maxTime);

        timer.value = fillValue;

        timer.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = gradient.Evaluate(fillValue);


        if (fillValue < 0.25 && isFlashing == false)
        {
            StartCoroutine(FlashingOverlay(recipe, warningColor, waitingTime));
            isFlashing = true;
        }

        if (fillValue < 0)
        {
            fillValue = 0;
            timerEnabled = false;
        }
    }

    IEnumerator FlashingOverlay(GameObject recipe, Color warningColor, float waitingTime)
    {
        while (true)
        {
            recipe.gameObject.transform.Find("UIOverlay").GetComponent<RawImage>().color = warningColor;

            yield return new WaitForSeconds(waitingTime);

            recipe.gameObject.transform.Find("UIOverlay").GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

            yield return new WaitForSeconds(waitingTime);
        }
    }

}
