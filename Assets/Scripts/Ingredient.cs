using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    // keep track of ingredient 
    [SerializeField] private IngType type;

    // [SerializeField] private Color mixColour = new Color(); 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public Color GetColor() {
    //     return mixColour;
    // }

    public IngType GetIngredientType() {
        return type;
    }
}
