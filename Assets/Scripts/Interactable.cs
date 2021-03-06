using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Interactable : MonoBehaviour {
    
    protected Material highlighter;
    protected Material actual;
    protected bool locked = false;

    protected virtual void Start() {
        highlighter = Resources.Load<Material>("Highlighter");
        actual = GetComponent<Renderer>().material; 
    }

    public virtual void OnContact() {
        if (!locked) {
            GetComponent<Renderer>().material = highlighter;
        }
    }

    public virtual void OnLeave() {
        if (!locked) {
            GetComponent<Renderer>().material = actual;
        }
    }

    public virtual void Interact(GameObject other) {}

    public bool IsLocked() {
        return locked;
    }

    public void Lock() {
        locked = true;
    }

    public void Unlock() {
        locked = false;
    }
} 