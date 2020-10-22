using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : Interactable
{

    // keep track of ingredient 
    public Processor.IngredientType type;

    // Start is called before the first frame update
    private bool held = false;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickup() {
        held = true;
        base.OnLeave();
    }

    public void OnDrop() {
        held = false;
    }

    public void OnDispense() {
        held = true;
    }

    public bool IsHeld() {
        return held;
    }
}
