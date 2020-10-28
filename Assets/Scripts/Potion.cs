using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Potion : Item
{
    //set to public, ask simon later
    [SerializeField] public Color potionColour;
    private GameObject potionLiquid;
    public Material opaqueLiquidMaterial;
    void Awake() {
        // Temporarily Set position to zero to ensure combining works properly
        Vector3 actualPosition = transform.position;
        transform.position = Vector3.zero;

        // Get actual potion object
        GameObject actualPotion = transform.GetChild(0).gameObject;

        MeshFilter[] meshFilters = actualPotion.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            // meshFilters[i].gameObject.SetActive(false);

            i++;
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
        // Get potion liquid object
        potionLiquid = transform.GetChild(0).GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPotionColor(Color colour) {
        potionColour = colour;

        potionLiquid.GetComponent<Renderer>().material = opaqueLiquidMaterial; // Temp fix
        potionLiquid.GetComponent<Renderer>().material.SetColor("_Color", potionColour);
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
