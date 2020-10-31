using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTree : MonoBehaviour
{
    // Start is called before the first frame update
    private LinkedList<RecipeNode> children = new LinkedList<RecipeNode>();
    private RecipeNode root = new RecipeNode(Processor.IngredientType.Null, Color.clear);
    void Start()
    {
        // bone -> chopped frog -> cheese
        RecipeNode BCCcheese = new RecipeNode(Processor.IngredientType.Cheese, Color.yellow);
        RecipeNode BCCchoppedFrog = new RecipeNode(Processor.IngredientType.ChoppedFrog, Color.green);
        BCCchoppedFrog.AddChild(BCCcheese);
        RecipeNode BCCBone = new RecipeNode(Processor.IngredientType.Bone, Color.white);
        BCCBone.AddChild(BCCchoppedFrog);

        // frog->burn flower->eye ball

        // chopped cheese->burnt bone->flower

        // burn flower->burnt frog->crushed eye ball

        // crushed bone->frog->cheese

        // eye ball->chopped cheese->bone

        // flower->burnt frog->burnt bone


        // cheese
        RecipeNode cheese = new RecipeNode(Processor.IngredientType.Cheese, Color.yellow);

        root.AddChild(BCCBone);
        root.AddChild(cheese);
        // root.AddFirst(Processor.IngredientType.Cheese, Color.yellow);
        // root.AddFirst(Processor.IngredientType.Bone, Color.white);
        // root.AddFirst(Processor.IngredientType.Flower, Color.magenta);
        // root.AddFirst(Processor.IngredientType.CharredFlower, Color.red);
        // root.AddFirst(Processor.IngredientType.Eyeball, Color.green);
        // root.AddFirst(Processor.IngredientType.MeltedBone, Color.black);






    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color FindColor(LinkedList<Processor.IngredientType> ingredients) {
        RecipeNode current = root;
        foreach (Processor.IngredientType ingredient in ingredients) {
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
    private Processor.IngredientType ingredientType;
    private LinkedList<RecipeNode> children;
    // private RecipeNode defaultNode;
    private Color color;

    public RecipeNode(Processor.IngredientType ingredientType, Color color)
    {
        this.ingredientType = ingredientType;
        this.color = color;
        this.children = new LinkedList<RecipeNode>();
    }

    public RecipeNode FindMatchingChild(Processor.IngredientType ingredientType)
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
     
    public void AddChild(Processor.IngredientType ingredientType, Color color)
    {
        children.AddFirst(new RecipeNode(ingredientType, color));
    }

    public void AddChild(RecipeNode node) {
        children.AddFirst(node);
    }
}