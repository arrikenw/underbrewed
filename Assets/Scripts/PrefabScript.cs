﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PrefabScript : MonoBehaviour
{
    // prefabs
    public GameObject bonePrefab;
    public GameObject meltedBonePrefab;
    public GameObject flowerPrefab;
    public GameObject charredFlowerPrefab;
    public GameObject cookedFrogPrefab;

    // particle system prefab
    public ParticleSystem psys;

    // lookup tables for cooking times
    public readonly Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, int> cooktimeMap
            = new Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, int>();

    // lookup tables for output ingredients
    public readonly Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, GameObject> ingredientMap
            = new Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, GameObject>();

    public int getFromCooktimeMap(Tuple<Processor.StationType, Processor.IngredientType> conditions)
    {
        return cooktimeMap[conditions];
    }

    public GameObject getFromIngredientMap(Tuple<Processor.StationType, Processor.IngredientType> conditions)
    {
        return ingredientMap[conditions];
    }

    void Start()
    {
        // Times
        // e.g.
        // cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Bone), 1000);
        // cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Flower), 15); //etc.

        // 650
        cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Bone), 600);
        cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Flower), 600);
        cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Frog), 600);

        // Outputs
        // e.g.
        // ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Bone), meltedBonePrefab);
        // ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Flower), charredFlowerPrefab); //etc.

        ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Bone), meltedBonePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Flower), charredFlowerPrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Frog), cookedFrogPrefab);
    }
}
