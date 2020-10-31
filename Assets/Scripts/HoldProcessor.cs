using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldProcessor : Processor
{

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!interacting)
        {
            return;
        }

        if (timeUntilComplete > 0)
        {
            timeUntilComplete -= 1;
        }

        //convert fully processed item
        if (timeUntilComplete == 0 && storedItem != null)
        {
            //create new object
            Tuple<StationType, IngredientType> lookupData = new Tuple<StationType, IngredientType>(station, currentIngredient);
            GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(lookupData), storedItem.transform.position, storedItem.transform.rotation);

            //destroy object being processed
            Destroy(storedItem);

            //change the stored item to the newly created object
            storedItem = processedOutput;

            //set kinematic to ensure item stays locked in place like the input ingredient
            storedItem.GetComponent<Rigidbody>().isKinematic = true;

            //todo add sound effect or something on completion

            //stop
            AttemptStopInteract();
        }
    }
}
