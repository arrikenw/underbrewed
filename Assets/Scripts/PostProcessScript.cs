using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//apply shader to processed image
public class PostProcessScript : MonoBehaviour
{
    public Material material;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}