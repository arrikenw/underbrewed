using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronBubblingSFX : MonoBehaviour
{
    public AudioSource BubblingSFX;
    // Start is called before the first frame update
    void Start()
    {
        BubblingSFX.loop = true;
        BubblingSFX.PlayDelayed(Random.Range(0.0f, 3.0f));
    }
}
