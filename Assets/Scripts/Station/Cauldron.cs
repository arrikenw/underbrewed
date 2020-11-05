using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Cauldron : Station
{
    public bool isTutorial;
    public GameObject tutorialController;

    public GameObject BadEventController;

    public AudioSource potionCollection;

    private GameObject cauldronLiquid;
    private GameObject bubbles;
    private GameObject bubbleBurst;
    [SerializeField] private Color mixColour; // Recording current colour is needed for future implementations
    private Color baseColour;

    private LinkedList<IngType> ingredients = new LinkedList<IngType>();
    [SerializeField] private GameObject recipeTree = null;

    private bool dud = false;
    private Color dudColour = Color.black;

    protected override void Start()
    {
        if (recipeTree == null) {
            //Debug.LogError("Assign a GameObject with recipeTree Script to Cauldron in the inspector before resuming");
            //UnityEditor.EditorApplication.isPlaying = false;
        }

        base.Start();

        // Get child objects
        cauldronLiquid = transform.Find("CauldronLiquid").gameObject;
        bubbles = cauldronLiquid.transform.Find("Bubbles").gameObject;  
        bubbleBurst = bubbles.transform.Find("BubbleBurst").gameObject;

        // Get colour values
        baseColour = cauldronLiquid.GetComponent<Renderer>().material.color;
        mixColour = baseColour;
    }


    void Update()
    {
        Mix();
    }

    private void Mix() {
        if (base.storedItem != null && base.storedItem.GetComponent<Ingredient>() != null) {
            if (dud) {
                Destroy(base.storedItem);
                if (isTutorial)
                {
                    tutorialController.GetComponent<TutorialScript>().OnBadRecipeCreated();
                }
                
                return;
            }

            // Retrieve new cauldron liquid colour from stored Item 
            // mixColour = base.storedItem.GetComponent<Ingredient>().GetColor();
            ingredients.AddLast(base.storedItem.GetComponent<Ingredient>().GetIngredientType());
            mixColour = recipeTree.GetComponent<RecipeTree>().FindColor(ingredients);

            // FAIL
            if (mixColour.Equals(Color.clear)) {
                mixColour = dudColour;
                ingredients.Clear();
                dud = true;
                BadEventController.GetComponent<BadEffects>().ApplyRandomEffect();
                print("CAULDRON FAIL");

                //tutorial integration for failures
                if (tutorialController)
                {
                    tutorialController.GetComponent<TutorialScript>().OnBadRecipeCreated();
                }
            }else
            {
                //tutorial integration
                if (tutorialController)
                {
                    //burnt bone is the tutorial recipe example
                    //it has a mix color of brown
                    if (mixColour == new Color(0.59f, 0.29f, 0.00f, 1.00f))
                    {
                        tutorialController.GetComponent<TutorialScript>().OnGoodRecipeCreated();
                    }
                }
            }

            // Destroy stored Item 
            Destroy(base.storedItem);
            
            // Update liquid and bubble colours
            UpdateColours();
        }
    }

    private void UpdateColours() {
        // Set new cauldron liquid colour
        cauldronLiquid.GetComponent<Renderer>().material.SetColor("_Color", mixColour);

        // Set new bubbles colour
        // https://docs.unity3d.com/ScriptReference/ParticleSystem.html
        // https://docs.unity3d.com/ScriptReference/ParticleSystem-main.html
        // Read above to understand why it must be written this way
        var bubblesMain = bubbles.GetComponent<ParticleSystem>().main;
        bubblesMain.startColor = mixColour;

        // Set new bubble burst colour
        var bubbleBurstMain = bubbleBurst.GetComponent<ParticleSystem>().main;
        bubbleBurstMain.startColor = mixColour;
    }

    public override void Interact(GameObject other) {
        print("Interacting with cauldron");

        Potion potion = other.GetComponent<Potion>();

        // Case: Potion
        if (!mixColour.Equals(baseColour) && potion && !potion.HasColor()) {
            potion.SetPotionColor(mixColour);
            potion.SetPotionIngredients(ingredients);

            potionCollection.Play();

            mixColour = baseColour;
            ingredients.Clear();
            dud = false;
            
            UpdateColours();
        }
    }

    protected override void OnTriggerStay(Collider other) {
        // Don't collected potions
        // Can refactor to ONLY collect ingredients
        if (other.gameObject.GetComponent<Potion>()) {
            return;
        } else {
            base.OnTriggerStay(other);
        }
    }

    public override bool TryDirectStore(Item item) {
        // Don't collected potions
        // Can refactor to ONLY collect ingredients
        if (item.gameObject.GetComponent<Potion>()) {
            return false;
        } else {
            return base.TryDirectStore(item);
        }
    }

}