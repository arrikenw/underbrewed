﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum IngType
    {
        Null,
        Bone,
        MeltedBone,
        CrushedBone,
        Flower,
        CharredFlower,
        Cheese,
        ChoppedCheese,
        Eyeball,
        CrushedEyeball,
        Frog,
        ChoppedFrog,
        CookedFrog,
        //etc.
    }

public class Processor : Station
{
    public enum StationType
    {
        Chop,
        Grill //etc.
    }

    // cooking logic
    protected bool interacting = false;
    protected int timeUntilComplete = 0;
    private IngType currentIngredient;

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
        if (cookEffects)
        {
            Destroy(cookEffects);
        }
    }

    public void AttemptStartInteract()
    {
        //don't begin interacting if no item is stored
        if (storedItem == null)
        {
            return;
        }

        //get IngType
        currentIngredient = storedItem.GetComponent<Ingredient>().type;

        //todo add correct type checks here
        if (currentIngredient != IngType.Bone && currentIngredient != IngType.Flower && currentIngredient != IngType.Frog)
        {
            print("item cannot be processed");
            return;
        }

        //get timer 
        timeUntilComplete = prefabManager.getFromCooktimeMap(new Tuple<StationType, IngType>(station, currentIngredient));
        
        //effects
        if (cookEffectsPrefab != null)
        {
            cookEffects = Instantiate(cookEffectsPrefab, transform.position, transform.rotation);
        }
        psys.Play();

        //object settings
        canPickup = false;
        interacting = true;
    }

}
