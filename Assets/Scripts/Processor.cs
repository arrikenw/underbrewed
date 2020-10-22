using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Processor : Station
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
    [SerializeField]
    public Material idleMat;
    [SerializeField]
    public Material cookMat;

    //progress
    private GameObject progressCanvas = null;
    private GameObject progressBar = null;
    private Slider bar = null;
    private int totalCooktime = 0;

    // cooking logic
    private int timeUntilComplete = 0;
    private bool isCooking = false;
    private IngredientType currentIngredient;

    // particle effects
    private ParticleSystem psys;

    //stationType
    [SerializeField]
    public StationType station;

    [SerializeField]
    public GameObject cookEffectsPrefab;
    private GameObject cookEffects;

    // prefabs
    private PrefabScript prefabManager;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        prefabManager = GameObject.FindGameObjectWithTag("PrefabManagerTag").GetComponent<PrefabScript>();
        psys = Instantiate(prefabManager.psys, transform.position, transform.rotation);
        psys.Stop();
        r = GetComponent<Renderer>();
        if (idleMat)
        {
            r.material = idleMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (station != StationType.Bench)
        {
            Cook();
        }
    }

    void Cook()
    {
        if (base.storedItem)
        {
            if (!isCooking)
            {
                //check new item type
                IngredientType currentIngredient = base.storedItem.GetComponent<Item>().type;

                //skip if the type is not a base ingredient
                if (currentIngredient != IngredientType.Bone && currentIngredient != IngredientType.Flower)
                {
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
                totalCooktime = prefabManager.getFromCooktimeMap(new Tuple<StationType, IngredientType>(station, currentIngredient)); //todo replace with ingredient type of stored object rather than just using bone
                timeUntilComplete = totalCooktime;
            }
            else
            {
                timeUntilComplete -= 1;
                //bar.value = ((float)timeUntilComplete) / totalCooktime;
                if (timeUntilComplete == 300)
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
                if (timeUntilComplete <= 0)
                {
                    isCooking = false;
                    base.canPickup = true;
                    if (r && idleMat)
                    {
                        r.material = idleMat;
                    }


                    //todo add sound effect or something on completion
                    //create new object
                    GameObject processedOutput = Instantiate(prefabManager.getFromIngredientMap(new Tuple<StationType, IngredientType>(station, currentIngredient)), base.storedItem.transform.position, base.storedItem.transform.rotation);

                    //destroy object being processed
                    Destroy(base.storedItem);
                    base.storedItem = null;

                    //change the stored item to the newly created object
                    base.storedItem = processedOutput;

                    //stop emmitting particles from the particle systems
                    psys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    
                    

                    //set kinematic to ensure item stays locked in place like the input ingredient
                    storedItem.GetComponent<Rigidbody>().isKinematic = true;


                    /*
                    //destroy progress bar
                    Destroy(progressCanvas);
                    progressCanvas = null;
                    progressBar = null;
                    bar = null;
                    */
                }
            }
        }
    }
}

/*
//***************************************************************************
//UI LMAO
//create canvas
GameObject progressCanvas = new GameObject();
progressCanvas.AddComponent(typeof(Canvas));
                progressCanvas.AddComponent(typeof(GraphicRaycaster));
                progressCanvas.AddComponent(typeof(CanvasScaler));
                Canvas curCanvas = progressCanvas.GetComponent(typeof(Canvas)) as Canvas;
curCanvas.renderMode = RenderMode.WorldSpace;
                
                progressCanvas.transform.parent = transform;
                progressCanvas.transform.position = transform.position;

                //create progressbar object
                GameObject progressBar = new GameObject();
progressBar.transform.parent = progressCanvas.transform;
                progressBar.AddComponent(typeof(Slider));
                bar = progressBar.GetComponent(typeof(Slider)) as Slider;
                bar.value = (float)timeUntilComplete / totalCooktime;
                print(bar.value);

//create rectangle area object
GameObject fillArea = new GameObject();
fillArea.transform.parent = progressBar.transform;
                fillArea.AddComponent(typeof(RectTransform));
                RectTransform rect = fillArea.GetComponent(typeof(RectTransform)) as RectTransform;
rect.anchorMin = new Vector2(0, 0.25f);
rect.anchorMax = new Vector2(1, 0.75f);
rect.offsetMin = new Vector2(0,0);
rect.offsetMax = new Vector2(0,0);
bar.fillRect = rect;

                //image/colour for filled area
                GameObject fillAreaImage = new GameObject();
fillAreaImage.transform.parent = fillArea.transform;
                fillAreaImage.AddComponent(typeof(Image));
                Image image1 = fillAreaImage.GetComponent(typeof(Image)) as Image;
image1.color = new Color(1, 0.5f, 0, 1);
fillAreaImage.AddComponent(typeof(RectTransform));

                RectTransform rect2 = fillAreaImage.GetComponent(typeof(RectTransform)) as RectTransform;
//no anchors
rect2.offsetMin = new Vector2(0, 0);
rect2.offsetMax = new Vector2(0, 0);
rect2.sizeDelta = new Vector2(100, 100);

//background image/colour
GameObject background = new GameObject();
background.transform.parent = progressBar.transform;
                background.AddComponent(typeof(Image));
                Image image2 = background.GetComponent(typeof(Image)) as Image;
image2.color = new Color(0, 1, 0.5f, 1);

//background rect
background.AddComponent(typeof(RectTransform));
                RectTransform rect3 = background.GetComponent(typeof(RectTransform)) as RectTransform;
rect3.anchorMin = new Vector2(0, 0.25f);
rect3.anchorMax = new Vector2(1, 0.75f);

rect3.offsetMin = new Vector2(0, 0);
rect3.offsetMax = new Vector2(0, 0);

//***************************************************************************
*/