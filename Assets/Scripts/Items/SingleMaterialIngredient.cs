using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMaterialIngredient : Ingredient
{   
    protected override void Awake() {
        GetComponent<MeshRenderer>().enabled = false;
    }

    protected override void Start()
    {
        highlighter = Resources.Load<Material>("Highlighter");
        actual = transform.GetChild(0).gameObject.GetComponentInChildren<Renderer>().material; 
    }


    public override void OnContact() {
        if (!locked) {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = highlighter;
            }
        }
    }

    public override void OnLeave() {
        if (!locked) {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = actual;
            }
        }
    }
}
