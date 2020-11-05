using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MenuMusic.Instance.StopMusic();
    }

}
