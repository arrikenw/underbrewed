using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronFireSFX : MonoBehaviour
{
    public AudioSource fireSFX;
    // Start is called before the first frame update
    void Start()
    {
        fireSFX.loop = true;
        fireSFX.PlayDelayed(Random.Range(0.0f, 3.0f));
    }

}
