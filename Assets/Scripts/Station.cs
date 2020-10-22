using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;





public class Station : Interactable
{

    public enum IngredientType
    {
        Bone,
        Flower,
        MeltedBone,
        CharredFlower //etc.
    }

    public enum StationType
    {
        Cauldron,
        Bench,
        Chop,
        Grill //etc.
    }

    // shading
    private Renderer r;
    [SerializeField] public Material idleMat;
    [SerializeField] public Material cookMat;

    //progress
    private GameObject progressCanvas = null;
    private GameObject progressBar = null;
    private Slider bar = null;
    private int totalCooktime = 0;

    // cooking logic
    private int timeUntilComplete = 0;
    private bool isCooking = false;

    // storage logic
    protected GameObject storedItem;
    protected bool canPickup = true;

    //stationType
    [SerializeField] public StationType station;
    public void setStationType(StationType stationType) {
        station = stationType;
    }

    // prefab
    private PrefabScript prefabManager;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        prefabManager = GameObject.FindGameObjectWithTag("PrefabManagerTag").GetComponent<PrefabScript>();
        print(prefabManager);
        r = GetComponent<Renderer>();
        if (idleMat)
        {
            r.material = idleMat;
        }   
    }

    // // Update is called once per frame
     public void Update()
     {
        if (station != StationType.Bench)
        {
            Cook();
        }
     }

    void Cook()
    {
        if (storedItem)
        {
            if (!isCooking)
            {
                print("HELLO");
                isCooking = true;
                canPickup = false;
                locked = true;

                // change mat / shader
                if (r && cookMat)
                {
                    r.material = cookMat;
                }


                // set cooking time
                print("STATION:");
                print(station);
                print("PREFAB: ");
                print(prefabManager);
                totalCooktime = prefabManager.getFromCooktimeMap(new Tuple<StationType, IngredientType>(station, IngredientType.Bone)); //todo replace with ingredient type of stored object rather than just using bone

                print("HELLO2");
                print("TOTAL COOKTIME: "+totalCooktime);

                print("END STATION");
                timeUntilComplete = totalCooktime;
            }
            else
            {
                timeUntilComplete -= 1;
                print(timeUntilComplete);
                //print(bar.value);
                //bar.value = ((float)timeUntilComplete) / totalCooktime;
                if (timeUntilComplete <= 0)
                {
                    isCooking = false;
                    canPickup = true;
                    if (r && idleMat)
                    {
                        r.material = idleMat;
                    }


                    //todo add sound effect or something on completion

                    //create new object
                    GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(new Tuple<StationType, IngredientType>(station, IngredientType.Bone)), storedItem.transform.position, storedItem.transform.rotation);

                    //destroy object being processed
                    Destroy(storedItem);
                    storedItem = null;

                    //change the stored item to the newly created object
                    storedItem = processedOutput;

                    //destroy progress bar
                    Destroy(progressCanvas);
                    progressCanvas = null;
                    progressBar = null;
                    bar = null;
                }
            }
        }
    }


    void OnTriggerStay(Collider other) {
        if (storedItem == null && other.gameObject.GetComponent<Item>() != null && !other.gameObject.GetComponent<Item>().IsHeld()) {
            print("Storing item in Station");
            storedItem = other.gameObject;
            storedItem.GetComponent<Rigidbody>().isKinematic = true;
            storedItem.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
            canPickup = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (storedItem != null && GameObject.ReferenceEquals(storedItem, other.gameObject)) {        
            print("Stored item moved from Station");
            storedItem.GetComponent<Rigidbody>().isKinematic = false;
            storedItem = null;
        }
    }

    public void OnPickup() {
        storedItem.GetComponent<Rigidbody>().isKinematic = false;
        storedItem.GetComponent<Item>().OnPickup();
        storedItem = null;
    }

    public GameObject TryPickup() {
        if (canPickup) {
            // GameObject tempItem = storedItem;
            // OnPickup();
            // return tempItem;
            return storedItem;
        } else {
            return null;
        }
    }
}
