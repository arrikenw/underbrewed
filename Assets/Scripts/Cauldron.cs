using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : Station
{
    private GameObject cauldronLiquid;
    private GameObject bubbles;
    private GameObject bubbleBurst;
    [SerializeField] private Color mixColour; // Recording current colour is needed for future implementations
    private Color baseColour;

    protected override void Start()
    {
        base.Start();
        cauldronLiquid = transform.GetChild(0).gameObject;
        bubbles = cauldronLiquid.transform.GetChild(0).gameObject;
        bubbleBurst = bubbles.transform.GetChild(0).gameObject;


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
            mixColour = base.storedItem.GetComponent<Ingredient>().GetColor();

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

        // Case: Potion
        if (!mixColour.Equals(baseColour) && other.GetComponent<Potion>() != null) {
            other.GetComponent<Potion>().SetPotionColor(mixColour);
            mixColour = baseColour;
            UpdateColours();
        }
    }


}
