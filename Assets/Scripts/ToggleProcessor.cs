using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ToggleProcessor : Processor
{
    // Start is called before the first frame update
    private bool interacting = false;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (interacting)
        {
            timeUntilComplete -= 1;
            if (timeUntilComplete == 350)
            {
                psys.Play();
            }

            if (timeUntilComplete == 0)
            {
                //remove flame etc.
                if (cookEffects)
                {
                    Destroy(cookEffects);
                }

                //create new object
                GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(new Tuple<StationType, IngredientType>(station, currentIngredient)), base.storedItem.transform.position, base.storedItem.transform.rotation);

                //destroy object being processed
                Destroy(base.storedItem);
                base.storedItem = null;

                //change the stored item to the newly created object
                base.storedItem = processedOutput;

                //set kinematic to ensure item stays locked in place like the input ingredient
                storedItem.GetComponent<Rigidbody>().isKinematic = true;

                //todo add sound effect or something on completion


                interacting = false;
                canPickup = true;
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
