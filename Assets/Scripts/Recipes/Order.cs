using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public Ingredient[] ingredients = new Ingredient[3]; //processed ingredients required;
    public float optimalPrepTime; //how long it takes to create recipe if actions are performed perfectly. idk, generate based off cooktimes for each food item?
    public Color targetColour;
    public int score;
}
