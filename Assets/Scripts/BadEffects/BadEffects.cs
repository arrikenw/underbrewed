using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEffects : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;
    public GameObject FrogCrate;
    public GameObject CheeseCrate;
    public GameObject EyeCrate;
    public GameObject PotionCrate;
    public GameObject BigFire;

    public void ApplyRandomEffect()
    {
        int effect;
        effect = Random.Range(0, 9);

        switch(effect)
        {
            case 0:
                // invert player movement
                Player.GetComponent<Player>().InvertMovement();
                break;
            case 1:
                // add screen shader
                print("screen effect");
                Camera.GetComponent<PostProcessScript>().ApplyEffect();
                break;
            case 2:
                print("frog explode");
                FrogCrate.GetComponent<ItemExplosion>().Explode();
                break;
            case 3:
                print("cheese explode");
                CheeseCrate.GetComponent<ItemExplosion>().Explode();
                break;
            case 4:
                print("big fire");
                BigFire.GetComponent<BigFireScript>().StartFire();
                break;
            case 5:
                print("potion explode");
                PotionCrate.GetComponent<ItemExplosion>().Explode();
                break;
            case 6:
                print("eyeball explode");
                EyeCrate.GetComponent<ItemExplosion>().Explode();
                break;
    
            default:
                // add screen shader
                print("screen effect");
                Camera.GetComponent<PostProcessScript>().ApplyEffect();
                break;
        }

    }
}
