using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{

    public Slider slider;

    public float fillValue = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //slider = gameObject.AddComponent(typeof(Slider)) as Slider;
        //slider.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        print(slider);
        print("updating slider");
        slider.value = fillValue;
    }

    // change value based on progress points 

    // change colour based on value
    
}
