using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
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

    public bool IsHeld() {
        return held;
    }
}
