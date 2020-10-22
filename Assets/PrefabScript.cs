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
    public readonly Dictionary<Tuple<Station.StationType, Station.IngredientType>, int> cooktimeMap
            = new Dictionary<Tuple<Station.StationType, Station.IngredientType>, int>();

    //lookup tables for output ingredients
    public readonly Dictionary<Tuple<Station.StationType, Station.IngredientType>, GameObject> ingredientMap
            = new Dictionary<Tuple<Station.StationType, Station.IngredientType>, GameObject>();

    public int getFromCooktimeMap(Tuple<Station.StationType, Station.IngredientType> conditions)
    {
        print("REACHED FUNC");
        return cooktimeMap[conditions];
    }

    public GameObject getFromIngredientMap(Tuple<Station.StationType, Station.IngredientType> conditions)
    {
        print("REACHED FUNC2");
        return ingredientMap[conditions];
    }

    void Start()
    {
        //times
        cooktimeMap.Add(new Tuple<Station.StationType, Station.IngredientType>(Station.StationType.Cauldron, Station.IngredientType.Bone), 1000);
        cooktimeMap.Add(new Tuple<Station.StationType, Station.IngredientType>(Station.StationType.Cauldron, Station.IngredientType.Flower), 15); //etc.

        cooktimeMap.Add(new Tuple<Station.StationType, Station.IngredientType>(Station.StationType.Grill, Station.IngredientType.Bone), 650);

        //outputs
        ingredientMap.Add(new Tuple<Station.StationType, Station.IngredientType>(Station.StationType.Cauldron, Station.IngredientType.Bone), meltedBonePrefab);
        ingredientMap.Add(new Tuple<Station.StationType, Station.IngredientType>(Station.StationType.Cauldron, Station.IngredientType.Flower), charredFlowerPrefab); //etc.

        ingredientMap.Add(new Tuple<Station.StationType, Station.IngredientType>(Station.StationType.Grill, Station.IngredientType.Bone), meltedBonePrefab);
    }
}
