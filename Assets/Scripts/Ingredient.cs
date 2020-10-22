using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    [SerializeField] private Color mixColour; 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color GetColor() {
        return mixColour;
    }
}
