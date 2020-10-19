using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItempRecipeController : MonoBehaviour
{
    public GameObject recipe;

    public GameObject timer;

    private UIRecipeTimer timerScript;

    private Color backgroundColor;

    private Color currentColor;

    //CanvasRenderer thisRenderer;


    void Start()
    {
        timerScript = timer.GetComponent<UIRecipeTimer>();
        backgroundColor = recipe.GetComponent<Image>().color;
        //currentColor = backgroundColor;
    }
    
    void Update()
    {

        if (timerScript.flashing == true)
        {
            Debug.Log("flashing on");

            if (recipe.GetComponent<Image>().color == Color.red)
            {
                recipe.GetComponent<Image>().color = backgroundColor;
            }
            else
            {
                recipe.GetComponent<Image>().color = Color.red;
            }
        }

        if (timerScript.timerEnabled == false)
        {
            //Destroy(this.gameObject);
            Debug.Log("End timer");
        }
        
    }
}
