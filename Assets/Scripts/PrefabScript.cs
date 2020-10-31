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

    // lookup tables for cooking times
    public readonly Dictionary<Tuple<Processor.StationType, IngType>, int> cooktimeMap
            = new Dictionary<Tuple<Processor.StationType, IngType>, int>();

    // lookup tables for output ingredients
    public readonly Dictionary<Tuple<Processor.StationType, IngType>, GameObject> ingredientMap
            = new Dictionary<Tuple<Processor.StationType, IngType>, GameObject>();

    public int getFromCooktimeMap(Tuple<Processor.StationType, IngType> conditions)
    {
        return cooktimeMap[conditions];
    }

    public GameObject getFromIngredientMap(Tuple<Processor.StationType, IngType> conditions)
    {
        return ingredientMap[conditions];
    }

    void Start()
    {
        // Times
        // e.g.
        // cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Cauldron, IngType.Bone), 1000);
        // cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Cauldron, IngType.Flower), 15); //etc.

        // 650
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Bone), 600);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Flower), 600);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Frog), 600);

        // Outputs
        // e.g.
        // ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Cauldron, IngType.Bone), meltedBonePrefab);
        // ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Cauldron, IngType.Flower), charredFlowerPrefab); //etc.

        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Bone), meltedBonePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Flower), charredFlowerPrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Frog), cookedFrogPrefab);
    }
}
