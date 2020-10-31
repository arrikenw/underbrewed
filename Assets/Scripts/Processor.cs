using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Processor : Station
{

    public enum IngredientType
    {
        Bone,
        Flower,
        MeltedBone,
        CharredFlower,
        Cheese,
        Eyeball,
        Frog,
        CookedFrog,
        Null//etc.
    }

    public enum StationType
    {
        Chop,
        Grill //etc.
    }

    // shading
    protected Renderer r;
    [SerializeField]
    public Material idleMat;
    [SerializeField]
    public Material cookMat;

    // cooking logic
    protected bool interacting = false;
    protected int timeUntilComplete = 0;
    protected IngredientType currentIngredient;

    // particle effects
    public ParticleSystem psysPrefab;
    protected ParticleSystem psys;

    //stationType
    [SerializeField]
    public StationType station;

    [SerializeField]
    public GameObject cookEffectsPrefab;
    protected GameObject cookEffects;

    // prefabs
    protected PrefabScript prefabManager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        prefabManager = GameObject.FindGameObjectWithTag("PrefabManagerTag").GetComponent<PrefabScript>();
        if (psysPrefab)
        {
            psys = Instantiate(psysPrefab, transform.position, transform.rotation);
            psys.Stop();
        }
        r = GetComponent<Renderer>();
        if (idleMat)
        {
            r.material = idleMat;
        }
    }

    public GameObject getStoredItem()
    {
        print(storedItem);
        print(base.storedItem);
        return storedItem;
    }

    public void setStoredItem(GameObject newItem)
    {
        storedItem = newItem;
        print("set new item to :" + storedItem);
        return;
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

}
