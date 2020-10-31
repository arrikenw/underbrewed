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
            //stop emmitting particles from the particle systems
            psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
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
