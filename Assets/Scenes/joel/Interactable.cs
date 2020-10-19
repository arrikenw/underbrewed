using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //object being processed
    public GameObject currentStoredObject = null;

    //skinning
    Renderer interactableRenderer;
    public Material idleMat = null;
    public Material cookMat = null;

    //logic
    private int timeUntilComplete = 0;
    private bool isCooking = false;

    // Start is called before the first frame update
    void Start()
    {
        interactableRenderer = GetComponent<Renderer>();
        interactableRenderer.material = idleMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooking)
        {
            timeUntilComplete -= 1;
            if (timeUntilComplete <= 0)
            {
                interactableRenderer.material = idleMat;

                //TODO output processed item and destroy held item
                isCooking = false;
            }
        }
    }

    public void interact(GameObject obj)
    {
        currentStoredObject = obj;
        
        //place above the object
        Vector3 newObjCoords = currentStoredObject.transform.position + (new Vector3(0, 0.5f, 0));
        currentStoredObject.transform.position = newObjCoords;

        //remove collision
        //todo

        //change mat / shader
        interactableRenderer.material = cookMat;

        //set cooking time etc.
        //TODO create a lookup table for the costs of each bench type with each item type
        int cookTime = 1000;
        timeUntilComplete = cookTime;
        isCooking = true; 
    }
}
