using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToggleProcessor : Processor
{
    protected override void Start()
    {
        base.Start();
        print(currentIngredient);
    }

    void Update()
    {
        if (interacting)
        {
            print(currentIngredient);
            timeUntilComplete -= 1;
            if (timeUntilComplete == 0)
            {
                //remove flame etc.
                if (cookEffects)
                {
                    Destroy(cookEffects);
                }

                //create new object
                Tuple<StationType, IngType> lookupData = new Tuple<StationType, IngType>(station, currentIngredient);
                print(lookupData);
                GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(lookupData), storedItem.transform.position, storedItem.transform.rotation);

                //destroy object being processed
                Destroy(storedItem);
                storedItem = null;

                //change the stored item to the newly created object
                storedItem = processedOutput;

                //set kinematic to ensure item stays locked in place like the input ingredient
                storedItem.GetComponent<Rigidbody>().isKinematic = true;

                // Run .OnStore()
                storedItem.GetComponent<Item>().OnStore(); // BUGFIX    

                //todo add sound effect or something on completion

                //stop
                AttemptStopInteract();
            }
        }
    }
}
