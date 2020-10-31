using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTree : MonoBehaviour
{
    private RecipeNode root = new RecipeNode(IngType.Null, Color.clear);
    void Start()
    {
        // A: bone -> chopped frog -> cheese
        RecipeNode ACheese = new RecipeNode(IngType.Cheese, Color.yellow);
        RecipeNode AChoppedFrog = new RecipeNode(IngType.ChoppedFrog, Color.green, ACheese);
        RecipeNode ABone = new RecipeNode(IngType.Bone, Color.white, AChoppedFrog);
        root.AddChild(ABone);

        // B: melted bone
        RecipeNode BMeltedBone = new RecipeNode(IngType.MeltedBone, new Color(0.59f,0.29f,0.00f,1.00f));
        root.AddChild(BMeltedBone);

        // C: crushed bone -> frog -> cheese
        RecipeNode CCheese = new RecipeNode(IngType.Cheese, Color.yellow);
        RecipeNode CFrog = new RecipeNode(IngType.Frog, Color.green, CCheese);
        RecipeNode CCrushedBone = new RecipeNode(IngType.CrushedBone, Color.white, CFrog);
        root.AddChild(CCrushedBone);

        // D: flower -> cooked frog -> melted bone
        RecipeNode DMeltedBone = new RecipeNode(IngType.MeltedBone, Color.yellow);
        RecipeNode DCookedFrog = new RecipeNode(IngType.CookedFrog, Color.green, DMeltedBone);
        RecipeNode DFlower = new RecipeNode(IngType.Flower, Color.white, DCookedFrog);
        root.AddChild(DFlower);

        // E: charred flower -> cooked frog -> crushed eyeball
        RecipeNode ECrushedEyeball = new RecipeNode(IngType.CrushedEyeball, Color.yellow);
        RecipeNode ECookedFrog = new RecipeNode(IngType.CookedFrog, Color.green, ECrushedEyeball);
        RecipeNode ECharredFlower = new RecipeNode(IngType.CharredFlower, Color.white, ECookedFrog);
        root.AddChild(ECharredFlower);

        // F: cheese
        RecipeNode FCheese = new RecipeNode(IngType.Cheese, Color.yellow);
        root.AddChild(FCheese);

        // G: chopped cheese -> melted bone -> flower
        RecipeNode GFlower = new RecipeNode(IngType.Flower, Color.magenta);
        RecipeNode GMeltedBone = new RecipeNode(IngType.MeltedBone, Color.white, GFlower);
        RecipeNode GChoppedCheese = new RecipeNode(IngType.ChoppedCheese, Color.white, GMeltedBone);
        root.AddChild(GChoppedCheese);

        // H: eyeball -> chopped cheese -> bone
        RecipeNode HBone = new RecipeNode(IngType.Bone, Color.white);
        RecipeNode HChoppedCheese = new RecipeNode(IngType.ChoppedCheese, Color.yellow, HBone);
        RecipeNode HEyeball = new RecipeNode(IngType.Eyeball, Color.white, HChoppedCheese);
        root.AddChild(HEyeball);    

        // I: crushed eyeball
        RecipeNode ICrushedEyeball = new RecipeNode(IngType.CrushedEyeball, Color.white);
        root.AddChild(ICrushedEyeball);   

        // J: frog-> charred flower -> eye ball
        RecipeNode JEyeball = new RecipeNode(IngType.Eyeball, Color.white);
        RecipeNode JCharredFlower = new RecipeNode(IngType.CharredFlower, Color.magenta, JEyeball);
        RecipeNode JFrog = new RecipeNode(IngType.Frog, Color.green, JCharredFlower);
        root.AddChild(JFrog);

        // K: chopped frog
        RecipeNode KChoppedFrog = new RecipeNode(IngType.ChoppedFrog, Color.white);
        root.AddChild(KChoppedFrog);   

        // L: cooked frog -> crushed bone -> crushed eyeball 
        RecipeNode LCrushedEyeball = new RecipeNode(IngType.Eyeball, Color.white);
        RecipeNode LCrushedBone = new RecipeNode(IngType.CharredFlower, Color.magenta, LCrushedEyeball);
        RecipeNode LCookedFrog = new RecipeNode(IngType.Frog, Color.green, LCrushedBone);
        root.AddChild(LCookedFrog);
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