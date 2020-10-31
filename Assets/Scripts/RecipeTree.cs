using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTree : MonoBehaviour
{
    // private LinkedList<RecipeNode> children = new LinkedList<RecipeNode>();
    private RecipeNode root = new RecipeNode(IngredientType.Null, Color.clear);
    void Start()
    {
        // A: bone -> chopped frog -> cheese
        RecipeNode ACheese = new RecipeNode(IngredientType.Cheese, Color.yellow);
        RecipeNode AChoppedFrog = new RecipeNode(IngredientType.ChoppedFrog, Color.green, ACheese);
        // BCCchoppedFrog.AddChild(BCCcheese);
        RecipeNode ABone = new RecipeNode(IngredientType.Bone, Color.white, AChoppedFrog);
        // BCCBone.AddChild(BCCchoppedFrog);
        root.AddChild(ABone);

        // B: melted bone
        RecipeNode BMeltedBone = new RecipeNode(IngredientType.MeltedBone, new Color(0.59f,0.29f,0.00f,1.00f));
        root.AddChild(BMeltedBone);

        // frog->burn flower->eye ball

        // chopped cheese->burnt bone->flower

        // burn flower->burnt frog->crushed eye ball

        // crushed bone->frog->cheese

        // eye ball->chopped cheese->bone

        // flower->burnt frog->burnt bone


        // cheese
        RecipeNode cheese = new RecipeNode(IngredientType.Cheese, Color.yellow);

        root.AddChild(cheese);


        // root.AddFirst(IngredientType.Cheese, Color.yellow);
        // root.AddFirst(IngredientType.Bone, Color.white);
        // root.AddFirst(IngredientType.Flower, Color.magenta);
        // root.AddFirst(IngredientType.CharredFlower, Color.red);
        // root.AddFirst(IngredientType.Eyeball, Color.green);
        // root.AddFirst(IngredientType.MeltedBone, Color.black);






    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color FindColor(LinkedList<IngredientType> ingredients) {
        RecipeNode current = root;
        foreach (IngredientType ingredient in ingredients) {
            current = current.FindMatchingChild(ingredient);
            if (current == null) {
                // Use Color.clear as if it means null
                return Color.clear;
            }
        }

        return current.GetColor();
    }
}

class RecipeNode {
    private IngredientType ingredientType;
    private LinkedList<RecipeNode> children;
    // private RecipeNode defaultNode;
    private Color color;

    public RecipeNode(IngredientType ingredientType, Color color)
    {
        this.ingredientType = ingredientType;
        this.color = color;
        this.children = new LinkedList<RecipeNode>();
    }

    public RecipeNode(IngredientType ingredientType, Color color, RecipeNode initialChild) {
        this.ingredientType = ingredientType;
        this.color = color;
        this.children = new LinkedList<RecipeNode>();
        AddChild(initialChild);
    }

    public RecipeNode FindMatchingChild(IngredientType ingredientType)
    {
        foreach (RecipeNode node in children)
        {
            if (ingredientType == node.ingredientType)
            {
                return node;
            }
        }
        Debug.Log("couldn't find a matching child");
        return null;
    }

    public Color GetColor() {
        return color;
    }
     
    public void AddChild(IngredientType ingredientType, Color color)
    {
        children.AddFirst(new RecipeNode(ingredientType, color));
    }

    public void AddChild(RecipeNode node) {
        children.AddFirst(node);
    }
}