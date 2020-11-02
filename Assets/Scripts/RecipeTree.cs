using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeTree : MonoBehaviour
{
    private RecipeNode root = new RecipeNode(IngType.Null, Color.clear);

    Color stageOneColor = new Color(0.69f,0.75f,0.10f,1.00f); // Acid Green
    Color stageTwoColor = new Color(0.95f,0.61f,0.73f,1.00f); // Amaranth Pink

    Color yellow = Color.yellow; //
    Color red = Color.red; //
    Color blue = Color.blue; //
    Color cyan = Color.cyan; //
    Color magenta = Color.magenta; //
    Color white = Color.white; //
    Color grey = Color.grey; //
    Color black = Color.black; //

    Color brown = new Color(0.59f,0.29f,0.00f,1.00f); //
    Color purple = new Color(0.35f,0.27f,0.70f,1.00f);
    Color darkGreen = new Color(0.11f,0.30f,0.24f,1.00f); //
    Color orange = new Color(1.00f,0.40f,0.00f,1.00f); //


    void Start()
    {
        // A: bone -> chopped frog -> cheese: white
        RecipeNode ACheese = new RecipeNode(IngType.Cheese, white);
        RecipeNode AChoppedFrog = new RecipeNode(IngType.ChoppedFrog, stageTwoColor, ACheese);
        RecipeNode ABone = new RecipeNode(IngType.Bone, stageOneColor, AChoppedFrog);
        root.AddChild(ABone);

        // B: melted bone: brown
        RecipeNode BMeltedBone = new RecipeNode(IngType.MeltedBone, brown);
        root.AddChild(BMeltedBone);

        // C: crushed bone -> frog -> cheese: black
        RecipeNode CCheese = new RecipeNode(IngType.Cheese, black);
        RecipeNode CFrog = new RecipeNode(IngType.Frog, stageTwoColor, CCheese);
        RecipeNode CCrushedBone = new RecipeNode(IngType.CrushedBone, stageOneColor, CFrog);
        root.AddChild(CCrushedBone);

        // D: flower -> cooked frog -> melted bone: grey
        RecipeNode DMeltedBone = new RecipeNode(IngType.MeltedBone, grey);
        RecipeNode DCookedFrog = new RecipeNode(IngType.CookedFrog, stageTwoColor, DMeltedBone);
        RecipeNode DFlower = new RecipeNode(IngType.Flower, stageOneColor, DCookedFrog);
        root.AddChild(DFlower);

        // E: charred flower -> cooked frog -> crushed eyeball: blue
        RecipeNode ECrushedEyeball = new RecipeNode(IngType.CrushedEyeball, blue);
        RecipeNode ECookedFrog = new RecipeNode(IngType.CookedFrog, stageTwoColor, ECrushedEyeball);
        RecipeNode ECharredFlower = new RecipeNode(IngType.CharredFlower, stageOneColor, ECookedFrog);
        root.AddChild(ECharredFlower);

        // F: cheese : yellow
        RecipeNode FCheese = new RecipeNode(IngType.Cheese, yellow);
        root.AddChild(FCheese);

        // G: chopped cheese -> melted bone -> flower: magenta
        RecipeNode GFlower = new RecipeNode(IngType.Flower, magenta);
        RecipeNode GMeltedBone = new RecipeNode(IngType.MeltedBone, stageTwoColor, GFlower);
        RecipeNode GChoppedCheese = new RecipeNode(IngType.ChoppedCheese, stageOneColor, GMeltedBone);
        root.AddChild(GChoppedCheese);

        // H: eyeball -> chopped cheese -> bone: cyan
        RecipeNode HBone = new RecipeNode(IngType.Bone, cyan);
        RecipeNode HChoppedCheese = new RecipeNode(IngType.ChoppedCheese, stageTwoColor, HBone);
        RecipeNode HEyeball = new RecipeNode(IngType.Eyeball, stageOneColor, HChoppedCheese);
        root.AddChild(HEyeball);    

        // I: crushed eyeball: dark green
        RecipeNode ICrushedEyeball = new RecipeNode(IngType.CrushedEyeball, darkGreen);
        root.AddChild(ICrushedEyeball);   

        // J: frog-> charred flower -> eye ball: purple
        RecipeNode JEyeball = new RecipeNode(IngType.Eyeball, purple);
        RecipeNode JCharredFlower = new RecipeNode(IngType.CharredFlower, stageTwoColor, JEyeball);
        RecipeNode JFrog = new RecipeNode(IngType.Frog, stageOneColor, JCharredFlower);
        root.AddChild(JFrog);

        // K: chopped frog: red
        RecipeNode KChoppedFrog = new RecipeNode(IngType.ChoppedFrog, red);
        root.AddChild(KChoppedFrog);   

        // L: cooked frog -> crushed bone -> crushed eyeball: orange
        RecipeNode LCrushedEyeball = new RecipeNode(IngType.Eyeball, orange);
        RecipeNode LCrushedBone = new RecipeNode(IngType.CharredFlower, stageTwoColor, LCrushedEyeball);
        RecipeNode LCookedFrog = new RecipeNode(IngType.Frog, stageOneColor, LCrushedBone);
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