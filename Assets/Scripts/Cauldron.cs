using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Cauldron : Station
{
    private GameObject cauldronLiquid;
    private GameObject bubbles;
    private GameObject bubbleBurst;
    [SerializeField] private Color mixColour; // Recording current colour is needed for future implementations
    private Color baseColour;

    private LinkedList<IngType> ingredients = new LinkedList<IngType>();
    [SerializeField] private GameObject recipeTree = null;

    protected override void Start()
    {
        if (recipeTree == null) {
            Debug.LogError("Assign a GameObject with recipeTree Script to Cauldron in the inspector before resuming");
            UnityEditor.EditorApplication.isPlaying = false;
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
            // Retrieve new cauldron liquid colour from stored Item 
            // mixColour = base.storedItem.GetComponent<Ingredient>().GetColor();
            ingredients.AddLast(base.storedItem.GetComponent<Ingredient>().GetIngredientType());
            mixColour = recipeTree.GetComponent<RecipeTree>().FindColor(ingredients);

            // FAIL
            if (mixColour.Equals(Color.clear)) {
                mixColour = baseColour;
                ingredients.Clear();
                print("CAULDRON FAIL");
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
        if (!mixColour.Equals(baseColour) && potion != null) {
            potion.SetPotionColor(mixColour);
            potion.SetPotionIngredients(ingredients);

            mixColour = baseColour;
            ingredients.Clear();
            
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

}