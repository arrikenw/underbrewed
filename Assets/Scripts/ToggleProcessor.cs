using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToggleProcessor : Processor
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    void Cook()
    {
        print(base.storedItem);
        print(base.getStoredItem());
        print("_____________________");
        if (base.storedItem && base.storedItem.GetComponent<Ingredient>())
        {
            //check new item type
            IngredientType currentIngredient = base.storedItem.GetComponent<Ingredient>().type;

            if (!isCooking)
            {

                //skip if the type is not a base ingredient
                if (currentIngredient != IngredientType.Bone && currentIngredient != IngredientType.Flower && currentIngredient != IngredientType.Frog)
                {
                    print("returning");
                    return;
                }

                isCooking = true;
                base.canPickup = false;
                locked = true;

                // change mat / shader
                if (r && cookMat)
                {
                    r.material = cookMat;
                }

                //activate cooking particle system
                //should actually be turning off and on, but our fire is an object so i can't switch on and off
                if (cookEffectsPrefab)
                {
                    cookEffects = Instantiate(cookEffectsPrefab, transform.position, transform.rotation);
                    cookEffects.transform.parent = transform;
                }

                // set cooking time
                timeUntilComplete = prefabManager.getFromCooktimeMap(new Tuple<StationType, IngredientType>(station, currentIngredient)); //todo replace with ingredient type of stored object rather than just using bone

            }
            else
            {
                timeUntilComplete -= 1;
                if (timeUntilComplete == 350)
                {
                    psys.Play();
                }
                if (timeUntilComplete == 100)
                {
                    if (cookEffects)
                    {
                        Destroy(cookEffects);
                    }
                }

                //convert slightly earlier than can be picked up to ensure transition is smoothly covered by particles
                if (timeUntilComplete == 100)
                {
                    //create new object
                    GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(new Tuple<StationType, IngredientType>(station, currentIngredient)), base.storedItem.transform.position, base.storedItem.transform.rotation);

                    //destroy object being processed
                    Destroy(base.storedItem);
                    base.storedItem = null;

                    //change the stored item to the newly created object
                    base.storedItem = processedOutput;

                    //set kinematic to ensure item stays locked in place like the input ingredient
                    storedItem.GetComponent<Rigidbody>().isKinematic = true;
                }

                if (timeUntilComplete <= 0)
                {
                    //todo add sound effect or something on completion
                    isCooking = false;
                    base.canPickup = true;
                    if (r && idleMat)
                    {
                        r.material = idleMat;
                    }

                    //stop emmitting particles from the particle systems
                    psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                }
            }
        }
    }
}
