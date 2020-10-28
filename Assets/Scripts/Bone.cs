using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Bone : Ingredient
{
    void Awake() {
        // Temporarily Set position to zero to ensure combining works properly
        Vector3 actualPosition = transform.position;
        transform.position = Vector3.zero;

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            // meshFilters[i].gameObject.SetActive(false);
        }

        GetComponent<MeshFilter>().mesh = new Mesh();
        GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        GetComponent<MeshRenderer>().enabled = false;

        transform.position = actualPosition;
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnContact() {
        GetComponent<MeshRenderer>().enabled = true;
        base.OnContact();
    }

    public override void OnLeave() {
        base.OnLeave();
        GetComponent<MeshRenderer>().enabled = false;
    }
}
