using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(MeshFilter))]
public class Potion : Item
{
    [SerializeField] Color potionColour;
    // void Awake() {
    //     Vector3 actualPosition = transform.position;
    //     // Vector3 actualScale = transform.localScale;
    //     // transform.localScale = Vector3.one;
    //     transform.position = Vector3.zero;

    //     MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
    //     CombineInstance[] combine = new CombineInstance[meshFilters.Length];

    //     int i = 0;
    //     while (i < meshFilters.Length)
    //     {
    //         combine[i].mesh = meshFilters[i].sharedMesh;
    //         combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
    //         // meshFilters[i].gameObject.SetActive(false);

    //         i++;
    //     }

    //     GetComponent<MeshFilter>().mesh = new Mesh();
    //     GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
    //     GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;

    //     // transform.localScale = actualScale;
    //     transform.position = actualPosition;
    //     transform.gameObject.SetActive(true);
    // }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPotionColor(Color colour) {
        potionColour = colour;

        // TEMP
        base.actual.SetColor("_Color", potionColour);
    }
}
