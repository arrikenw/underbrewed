using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//apply shader to processed image
public class PostProcessScript : MonoBehaviour
{
    public Material material;
    public AudioSource screenEffectSound;

    private bool applyAffect = false;
    private float affectCounter = 0.0f;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (applyAffect)
        {
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void ApplyEffect()
    {
        applyAffect = true;
        affectCounter = 10.0f;
        screenEffectSound.Play();
    }

    void Update()
    {
        if (affectCounter <= 0.0f)
        {
            applyAffect = false;
            screenEffectSound.Stop();
        }
        else
        {
            affectCounter -= Time.deltaTime;
        }
    }
}
