using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PrefabScript : MonoBehaviour
{
    // prefabs

    //defaults
    public GameObject bonePrefab;
    public GameObject frogPrefab;
    public GameObject flowerPrefab;
    public GameObject cheesePrefab;
    public GameObject eyeballPrefab;

    //grill
    public GameObject meltedBonePrefab;
    public GameObject charredFlowerPrefab;
    public GameObject cookedFrogPrefab;

    //crush
    public GameObject crushedBonePrefab;
    public GameObject crushedEyeballPrefab;

    //chop
    public GameObject choppedCheesePrefab;
    public GameObject choppedFrogPrefab;
    

    // lookup tables for cooking times
    public readonly Dictionary<Tuple<Processor.StationType, IngType>, float> cooktimeMap
            = new Dictionary<Tuple<Processor.StationType, IngType>, float>();

    // lookup tables for output ingredients
    public readonly Dictionary<Tuple<Processor.StationType, IngType>, GameObject> ingredientMap
            = new Dictionary<Tuple<Processor.StationType, IngType>, GameObject>();

    public float getFromCooktimeMap(Tuple<Processor.StationType, IngType> conditions)
    {
        //return error value if the key is invalid
        if (!cooktimeMap.ContainsKey(conditions)){
            return -1.0f;
        }
        return cooktimeMap[conditions];
    }

    public GameObject getFromIngredientMap(Tuple<Processor.StationType, IngType> conditions)
    {
        return ingredientMap[conditions];
    }

    void Start()
    {
        // Times

        //Cooking
        //TODO VARY TIMES
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Bone), 3);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Flower), 3);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Frog), 3);

        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Chop, IngType.Frog), 3);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Chop, IngType.Cheese), 3);

        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Crush, IngType.Eyeball), 3);
        cooktimeMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Crush, IngType.Bone), 3);

        // Outputs

        //cooking
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Bone), meltedBonePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Flower), charredFlowerPrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Grill, IngType.Frog), cookedFrogPrefab);

        //chopping
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Chop, IngType.Cheese), choppedCheesePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Chop, IngType.Frog), choppedFrogPrefab);

        //crushing
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Crush, IngType.Bone), crushedBonePrefab);
        ingredientMap.Add(new Tuple<Processor.StationType, IngType>(Processor.StationType.Crush, IngType.Eyeball), crushedEyeballPrefab);

    }
}
