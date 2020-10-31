using System.Collections;
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
    public readonly Dictionary<Tuple<Processor.StationType, IngredientType>, int> cooktimeMap
            = new Dictionary<Tuple<Processor.StationType, IngredientType>, int>();

    // lookup tables for output ingredients
    public readonly Dictionary<Tuple<Processor.StationType, IngredientType>, GameObject> ingredientMap
            = new Dictionary<Tuple<Processor.StationType, IngredientType>, GameObject>();

    public int getFromCooktimeMap(Tuple<Processor.StationType, IngredientType> conditions)
    {
        return cooktimeMap[conditions];
    }

    public GameObject getFromIngredientMap(Tuple<Processor.StationType, IngredientType> conditions)
    {
        return ingredientMap[conditions];
    }

    void Start()
    {
        // Times
        // e.g.
        // cooktimeMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Cauldron, IngredientType.Bone), 1000);
        // cooktimeMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Cauldron, IngredientType.Flower), 15); //etc.

        // 650
        cooktimeMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Grill, IngredientType.Bone), 600);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Grill, IngredientType.Flower), 600);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Grill, IngredientType.Frog), 600);

        // Outputs
        // e.g.
        // ingredientMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Cauldron, IngredientType.Bone), meltedBonePrefab);
        // ingredientMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Cauldron, IngredientType.Flower), charredFlowerPrefab); //etc.

        ingredientMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Grill, IngredientType.Bone), meltedBonePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Grill, IngredientType.Flower), charredFlowerPrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngredientType>(Processor.StationType.Grill, IngredientType.Frog), cookedFrogPrefab);
    }
}
