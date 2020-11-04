﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
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
        base.Unlock();
        OnLeave();
    }

    public void OnDrop() {
        held = false;
        base.Unlock();
    }

    public void OnDispense() {
        held = true;
    }

    public void OnStore() {
        OnLeave();
        base.Lock();
    }

    public void OnDirectStore() {
        held = false;
        base.Lock();
    }

    public bool IsHeld() {
        return held;
    }
}
