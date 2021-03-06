using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class Ingredient : Item
{
    // keep track of ingredient 
    [SerializeField] public IngType ingredientType = IngType.Null;

    private bool usesCombinedMesh = false;

    // [SerializeField] private bool canHighlight = true;
    
    protected virtual void Awake() {
        if (GetComponent<MeshFilter>().sharedMesh == null) {
            CombineMesh();
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update() 
    {
        // if below the floor, destroy itself
        if (transform.position.y < -3.0f)
        {
            Destroy(this.gameObject);
        }

        // lifetime TODO
        /*if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            lifetime -= 
        }*/
    }


    public IngType GetIngredientType() {
        return ingredientType;
    }

    private void CombineMesh() {
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

        // Assign combined mesh to mesh collider if it exists
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (meshCollider) {
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
        }

        GetComponent<MeshRenderer>().enabled = false;

        // Restore position
        transform.position = actualPosition;

        usesCombinedMesh = true;
    }

    public override void OnContact() {
        if (usesCombinedMesh) {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].enabled = false;
            }

            GetComponent<MeshRenderer>().enabled = true;
        }
        base.OnContact();
    }

    public override void OnLeave() {
        if (usesCombinedMesh) {
            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].enabled = true;
            }

            GetComponent<MeshRenderer>().enabled = false;
        }
        base.OnLeave();
    }
}
