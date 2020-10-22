using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempRecipeQueue : MonoBehaviour
{
    public GameObject recipe1;
    public GameObject recipe2;
    public GameObject recipe3;

    public bool FlashRed;

    public GameObject redRecipe1;
    public GameObject redRecipe2;
    public GameObject redRecipe3;

    public float width = 128.5f;
    public float xPosition = 79.5f;
    public float yPosition = 345.5f;
    //public float zPosition;
    public KeyCode key = KeyCode.K;
    private int n;

    // Start is called before the first frame update
    void Start()
    {
        n = 0;
        //zPosition = recipe.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(key))
        {
            if (FlashRed == false)
            {
                RecipeController(n);
            }
            else
            {
                redRecipeController(n);
            }
            
            //var newRecipe = Instantiate(recipe, new Vector3(xPosition - (n*width), yPosition, zPosition), recipe.transform.rotation, gameObject.transform);
            //newRecipe.transform.parent = gameObject.transform;
            n += 1; 
        }
    }

    void RecipeController(int n)
    {
        switch (n)
        {
            case 0:
                InsertRecipe(n, recipe1);
                break;
            case 1:
                InsertRecipe(n, recipe1);
                break;
            case 2:
                InsertRecipe(n, recipe2);
                break;
            case 3:
                InsertRecipe(n, recipe2);
                break;
            case 4:
                InsertRecipe(n, recipe3);
                break;
        }
    }


    void redRecipeController(int n)
    {
        switch (n)
        {
            case 0:
                InsertRecipe(n, redRecipe1);
                break;
            case 1:
                InsertRecipe(n, redRecipe1);
                break;
            case 2:
                InsertRecipe(n, redRecipe2);
                break;
            case 3:
                InsertRecipe(n, redRecipe2);
                break;
            case 4:
                InsertRecipe(n, redRecipe3);
                break;
        }
    }


    void InsertRecipe(int n, GameObject recipe)
    {
        var a = Instantiate(recipe, gameObject.transform, false);
        Vector3 aPosition = a.transform.localPosition;
        aPosition.x += xPosition + (width * n);
        aPosition.y += yPosition;
        a.transform.localPosition = aPosition;
    }
}
