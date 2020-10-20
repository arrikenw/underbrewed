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

    //public GameObject overlay;

    void Start()
    {
        //recipe = gameObject;

        timerScript = timer.GetComponent<UIRecipeTimer>();

    }
    
    void Update()
    {

        // simulate if order was completed in time
        if (Input.GetKeyUp("space"))
        {
            //TO DO: flash background green
            timerScript.timerEnabled = false;
        }

        if (timerScript.timerEnabled == false)
        {
            Destroy(this.gameObject);
        }
        
    }

}


