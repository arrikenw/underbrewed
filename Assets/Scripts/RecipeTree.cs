using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTree : MonoBehaviour
{
    // Start is called before the first frame update
    private LinkedList<RecipeNode> children = new LinkedList<RecipeNode>();
    private RecipeNode root = new RecipeNode(Processor.IngredientType.Potion, Color.clear);
    void Start()
    {
        
        root.AddChild(Processor.IngredientType.Cheese, Color.yellow);
        root.AddChild(Processor.IngredientType.Bone, Color.white);
        root.AddChild(Processor.IngredientType.Flower, Color.magenta);
        root.AddChild(Processor.IngredientType.CharredFlower, Color.red);
        root.AddChild(Processor.IngredientType.Eyeball, Color.green);
        root.AddChild(Processor.IngredientType.MeltedBone, Color.black);
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
}