using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFireScript : MonoBehaviour
{
    private float EffectCount = 0.0f;

    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void StartFire()
    {
        this.gameObject.SetActive(true);
        EffectCount = 10.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (EffectCount <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            EffectCount -= Time.deltaTime;
        }
    }
}
