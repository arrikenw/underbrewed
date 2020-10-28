using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCauldron : Station
{
    // // object being processed
    // private GameObject currentStoredObject;

    // shading
    Renderer r;
    [SerializeField] private Material idleMat = null;
    [SerializeField] private Material cookMat = null;

    // logic
    // TODO create a lookup table for the costs of each bench type with each item type
    [SerializeField] private int cookTime = 3; 
    // private int timeUntilComplete = 0;
    private bool isCooking = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        r = GetComponent<Renderer>();
        r.material = idleMat;
    }

    // Update is called once per frame
    void Update()
    {
        Cook();
    }

    void Cook() {

        if (!isCooking) {
            if (base.storedItem != null) {
                base.canPickup = false;
                base.locked = true;

                // TODO: remove collision

                // change mat / shader
                r.material = cookMat;

                // set cooking time etc.
                // timeUntilComplete = cookTime; 
                // Note for Joel: logic doesn't have to be done this way, I just wanted to try this hack
                Destroy(base.storedItem, cookTime);
                isCooking = true; 
            }
        } else {
            
            if (base.storedItem == null)
            {
                r.material = idleMat;

                // TODO: output processed item
                isCooking = false;
            }

            // timeUntilComplete -= 1;
            // if (timeUntilComplete <= 0)
            // {
            //     r.material = idleMat;

            //     // TODO: output processed item and destroy held item
            //     isCooking = false;
            // }
        }
    }

    // public void Interact(GameObject obj)
    // {
    //     // currentStoredObject = obj;
        
    //     //place above the object
    //     Vector3 newObjCoords = currentStoredObject.transform.position + (new Vector3(0, 0.5f, 0));
    //     currentStoredObject.transform.position = newObjCoords;

    //     // TODO: remove collision

    //     // change mat / shader
    //     r.material = cookMat;

    //     // set cooking time etc.
    //     timeUntilComplete = cookTime;
    //     isCooking = true; 
    // }
}
