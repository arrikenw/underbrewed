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

    //lookup tables for cooking time
    //TODO rewrite to work based on class? idk 
    public readonly Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, int> cooktimeMap
            = new Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, int>();

    //lookup tables for output ingredients
    public readonly Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, GameObject> ingredientMap
            = new Dictionary<Tuple<Processor.StationType, Processor.IngredientType>, GameObject>();

    public int getFromCooktimeMap(Tuple<Processor.StationType, Processor.IngredientType> conditions)
    {
        print("REACHED FUNC");
        return cooktimeMap[conditions];
    }

    public GameObject getFromIngredientMap(Tuple<Processor.StationType, Processor.IngredientType> conditions)
    {
        print("REACHED FUNC2");
        return ingredientMap[conditions];
    }

    void Start()
    {
        //times
        cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Bone), 1000);
        cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Flower), 15); //etc.

        cooktimeMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Bone), 650);

        //outputs
        ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Bone), meltedBonePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Cauldron, Processor.IngredientType.Flower), charredFlowerPrefab); //etc.

        ingredientMap.Add(new Tuple<Processor.StationType, Processor.IngredientType>(Processor.StationType.Grill, Processor.IngredientType.Bone), meltedBonePrefab);
    }
}
