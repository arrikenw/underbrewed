using System.Collections;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    private Material highlighter;
    private Material original;
    protected Material highlighter;
    protected Material actual;
    protected bool locked = false;

    protected virtual void Start() {
        highlighter = Resources.Load<Material>("Highlighter");
        original = GetComponent<Renderer>().material; 
        actual = GetComponent<Renderer>().material; 
    }

    public void OnContact() {
        if (!locked) {
            GetComponent<Renderer>().material = highlighter;
        }
    }

    public void OnLeave() {
        if (!locked) {
            GetComponent<Renderer>().material = original;
            GetComponent<Renderer>().material = actual;
        }
    }

    public virtual void Interact() {}
    public virtual void Interact(GameObject other) {}
} 