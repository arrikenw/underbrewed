using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class interactableScript : MonoBehaviour
{
    enum cookState { Cooking, Complete, Empty }
    int cookTime;
    GameObject inputItem = null;
    cookState currentState = cookState.Empty;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cookTime -= 1;
        if (currentState == cookState.Cooking && cookTime == 0)
        {
            currentState = cookState.Complete;
            inputItem = getTransIngredient(inputItem);
            playCompletionEffect();
        }
    }
}
