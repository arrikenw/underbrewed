using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTree : MonoBehaviour
{
    // private LinkedList<RecipeNode> children = new LinkedList<RecipeNode>();
    private RecipeNode root = new RecipeNode(IngType.Null, Color.clear);
    void Start()
    {
        // A: bone -> chopped frog -> cheese
        RecipeNode ACheese = new RecipeNode(IngType.Cheese, Color.yellow);
        RecipeNode AChoppedFrog = new RecipeNode(IngType.ChoppedFrog, Color.green, ACheese);
        // BCCchoppedFrog.AddChild(BCCcheese);
        RecipeNode ABone = new RecipeNode(IngType.Bone, Color.white, AChoppedFrog);
        // BCCBone.AddChild(BCCchoppedFrog);
        root.AddChild(ABone);

        // B: melted bone
        RecipeNode BMeltedBone = new RecipeNode(IngType.MeltedBone, new Color(0.59f,0.29f,0.00f,1.00f));
        root.AddChild(BMeltedBone);

        // C: crushed bone -> frog -> cheese
        // RecipeNode CCheese = new RecipeNode(IngType.Cheese, Color.yellow);
        // RecipeNode CChoppedFrog = new RecipeNode(IngType.ChoppedFrog, Color.green, CCheese);
        // // BCCchoppedFrog.AddChild(BCCcheese);
        // RecipeNode CBone = new RecipeNode(IngType.Bone, Color.white, CChoppedFrog);
        // // BCCBone.AddChild(BCCchoppedFrog);
        // root.AddChild(CBone);

        // frog->burn flower->eye ball

        // chopped cheese->burnt bone->flower

        // burn flower->burnt frog->crushed eye ball

        // crushed bone->frog->cheese

        // eye ball->chopped cheese->bone

        // flower->burnt frog->burnt bone


        // cheese
        RecipeNode cheese = new RecipeNode(IngType.Cheese, Color.yellow);

        root.AddChild(cheese);


        // root.AddFirst(IngType.Cheese, Color.yellow);
        // root.AddFirst(IngType.Bone, Color.white);
        // root.AddFirst(IngType.Flower, Color.magenta);
        // root.AddFirst(IngType.CharredFlower, Color.red);
        // root.AddFirst(IngType.Eyeball, Color.green);
        // root.AddFirst(IngType.MeltedBone, Color.black);






    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color FindColor(LinkedList<IngType> ingredients) {
        RecipeNode current = root;
        foreach (IngType ingredient in ingredients) {
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
    private IngType IngType;
    private LinkedList<RecipeNode> children;
    // private RecipeNode defaultNode;
    private Color color;

    public RecipeNode(IngType IngType, Color color)
    {
        this.IngType = IngType;
        this.color = color;
        this.children = new LinkedList<RecipeNode>();
    }

    public RecipeNode(IngType IngType, Color color, RecipeNode initialChild) {
        this.IngType = IngType;
        this.color = color;
        this.children = new LinkedList<RecipeNode>();
        AddChild(initialChild);
    }

    public RecipeNode FindMatchingChild(IngType IngType)
    {
        foreach (RecipeNode node in children)
        {
            if (IngType == node.IngType)
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
     
    public void AddChild(IngType IngType, Color color)
    {
        children.AddFirst(new RecipeNode(IngType, color));
    }

    public void AddChild(RecipeNode node) {
        children.AddFirst(node);
    }
}