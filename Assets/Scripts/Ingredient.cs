﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    // keep track of ingredient 
    [SerializeField] public IngType ingredientType = IngType.Null;

    // [SerializeField] private Color mixColour = new Color(); 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -3.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // public Color GetColor() {
    //     return mixColour;
    // }

    public IngType GetIngredientType() {
        return ingredientType;
    }
}
