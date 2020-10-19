using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    private Material highlighter;
    private Material original;
    protected bool locked = false;

    protected virtual void Start() {
        highlighter = Resources.Load<Material>("Highlighter");
        print(highlighter);
        original = GetComponent<Renderer>().material; 
    }

    public void OnContact() {
        if (!locked) {
            GetComponent<Renderer>().material = highlighter;
        }
    }

    public void OnLeave() {
        if (!locked) {
            GetComponent<Renderer>().material = original;
        }
    }

    public virtual void Interact() {}
} 