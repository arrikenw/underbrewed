using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    
    private Material highlighter;
    private Material original;

    public virtual void Start() {
        highlighter = Resources.Load<Material>("Highlighter");
        print(highlighter);
        original = GetComponent<Renderer>().material; 
    }

    public void OnContact() {
        GetComponent<Renderer>().material = highlighter;
    }

    public void OnLeave() {
        GetComponent<Renderer>().material = original;
    }
} 