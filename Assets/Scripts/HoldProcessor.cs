using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldProcessor : Processor
{
    private bool interacting = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!interacting)
        {
            //stop emmitting particles from the particle systems
            psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }


    public bool getInteract()
    {
        return interacting;
    }

    public void AttemptStopInteract()
    {
        interacting = false;
        canPickup = true;
        psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    public void AttemptStartInteract()
    {
        //don't begin interacting if no item is stored
        if (storedItem == null)
        {
            return;
        }

        //get ingredientType
        IngredientType currentIngredient = storedItem.GetComponent<Ingredient>().type;

        //todo add correct type checks here
        if (currentIngredient != IngredientType.Bone && currentIngredient != IngredientType.Flower && currentIngredient != IngredientType.Frog)
        {
            print("item cannot be processed");
            return;
        }

        //get timer 
        timeUntilComplete = prefabManager.getFromCooktimeMap(new Tuple<StationType, IngredientType>(station, currentIngredient));

        psys.Play();
        canPickup = false;
        interacting = true;
    }

        

    public void AttemptInteract()
    {
        if (!interacting)
        {
            return;
        }

        if (timeUntilComplete > 0)
        {
            print(timeUntilComplete);   
            timeUntilComplete -= 1;
        }

        //convert fully processed item
        if (timeUntilComplete == 0 && storedItem != null)
        {
            //create new object
            GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(new Tuple<StationType, IngredientType>(station, currentIngredient)), storedItem.transform.position, storedItem.transform.rotation);

            //destroy object being processed
            Destroy(storedItem);

            //change the stored item to the newly created object
            storedItem = processedOutput;

            //set kinematic to ensure item stays locked in place like the input ingredient
            storedItem.GetComponent<Rigidbody>().isKinematic = true;

            //todo add sound effect or something on completion
            canPickup = true;
            locked = false; //idk if this does anything

            //stop emmitting particles from the particle systems
            psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            //stop interacting
            interacting = false;
        }
    }
}
