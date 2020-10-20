using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camsetup : MonoBehaviour
{
    public Shader camShader = null;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.SetReplacementShader(camShader, "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
