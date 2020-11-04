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
    public GameObject handKnife;
    public GameObject handPestle;
    public AudioSource soundEffect;
    public AudioSource finishedSound;
    
    public enum StationType
    {
        Chop,
        Grill,
        Crush//etc.
    }

    // tutorial
    public GameObject tutorialController;

    // cooking logic
    protected bool interacting = false;
    public float timeUntilComplete = 0;
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
        if (station == StationType.Chop) {
            // Override Highlighting
            highlighter = Resources.Load<Material>("Highlighter");
            actual = transform.Find("ChoppingBoard").gameObject.GetComponent<Renderer>().material;  
        }
        else if (station == StationType.Crush) {
            // Override Highlighting
            highlighter = Resources.Load<Material>("Highlighter");
            actual = transform.Find("Mortar").gameObject.GetComponent<Renderer>().material; 
        } else {
            base.Start();
        }

        // Processor specific stuff
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
        locked = false;
        if (psys)
        {
            psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
        
        if (cookEffects)
        {
            Destroy(cookEffects);
        }

        soundEffect.Stop();
        animator.Play("EmptyIdle");
        if (station == StationType.Chop)
        {
            handKnife.SetActive(false);
            transform.Find("Knife").gameObject.SetActive(true);
        }
        else if (station == StationType.Crush)
        {
            handPestle.SetActive(false);
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

        //object settings
        interacting = true;
        OnLeave(); // Removes highlighting
        canPickup = false; // Prevents pickup
        locked = true; // Prevents highlighting

        soundEffect.Play();
        if (station == StationType.Chop)
        {
            handKnife.SetActive(true);
            animator.Play("ChopAnim");
            transform.Find("Knife").gameObject.SetActive(false);
        }
        else if (station == StationType.Crush)
        {
            handPestle.SetActive(true);
            animator.Play("PoundAnim");
            transform.Find("pestle").gameObject.SetActive(false);
        }

        
    }

    public override void OnContact() {
        if (!locked) {
            if (station == StationType.Chop) {
                // Override Highlighting
                transform.Find("ChoppingBoard").gameObject.GetComponent<Renderer>().material = highlighter;
            }
            else if (station == StationType.Crush) {
                // Override Highlighting
                transform.Find("Mortar").gameObject.GetComponent<Renderer>().material = highlighter;
            } else {
                base.OnContact();
            }
        }
    }

    public override void OnLeave() {
        if (!locked) {
            if (station == StationType.Chop) {
                // Override Highlighting
                transform.Find("ChoppingBoard").gameObject.GetComponent<Renderer>().material = actual;
            }
            else if (station == StationType.Crush) {
                // Override Highlighting
                transform.Find("Mortar").gameObject.GetComponent<Renderer>().material = actual;
            } else {
                base.OnLeave();
            }
        }
    }
    
}
