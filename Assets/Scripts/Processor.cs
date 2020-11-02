using System.Collections;
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
    public Animator animator;
    public GameObject knife;
    public GameObject pestle;
    public enum StationType
    {
        Chop,
        Grill,
        Crush//etc.
    }

    // cooking logic
    protected bool interacting = false;
    protected float timeUntilComplete = 0;
    protected IngType currentIngredient;

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
        if (psys)
        {
            psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        
        if (cookEffects)
        {
            Destroy(cookEffects);
        }

        animator.Play("EmptyIdle");
        if (station == StationType.Chop)
        {
            knife.SetActive(false);
            transform.Find("Knife_01").gameObject.SetActive(true);
        }
        else if (station == StationType.Crush)
        {
            pestle.SetActive(false);
            transform.Find("pestle").gameObject.SetActive(true);
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
        currentIngredient = storedItem.GetComponent<Ingredient>().GetIngredientType();

        //get timer 
        timeUntilComplete = prefabManager.getFromCooktimeMap(new Tuple<StationType, IngType>(station, currentIngredient));
        
        //lookup failed
        if (timeUntilComplete == -1.0f)
        {
            print("this station cannot process: " + currentIngredient);
            return;
        }

        //effects
        if (cookEffectsPrefab != null)
        {
            cookEffects = Instantiate(cookEffectsPrefab, transform.position, transform.rotation);
        }

        if (psys)
        {
            psys.Play();
        }
       
        if (station == StationType.Chop)
        {
            knife.SetActive(true);
            animator.Play("ChopAnim");
            transform.Find("Knife_01").gameObject.SetActive(false);
        }
        else if (station == StationType.Crush)
        {
            pestle.SetActive(true);
            animator.Play("PoundAnim");
            transform.Find("pestle").gameObject.SetActive(false);
        }

        //object settings
        canPickup = false;
        interacting = true;
    }

}
