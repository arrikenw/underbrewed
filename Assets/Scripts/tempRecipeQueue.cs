using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempRecipeQueue : MonoBehaviour
{
    public GameObject recipe;
    public float width = 128.5f;
    public float xPosition = 79.5f;
    public float yPosition = 345.5f;
    public KeyCode key = KeyCode.K;
    private float n;

    // Start is called before the first frame update
    void Start()
    {
        n = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            var newRecipe = Instantiate(recipe, new Vector3(xPosition + (n*width), yPosition, 0), Quaternion.identity, gameObject.transform);
            //newRecipe.transform.parent = gameObject.transform;
            n += 1; 
        }
    }
}
