using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplosion : MonoBehaviour
{
    public void Explode()
    {
        for (int i = 0; i < 6; i++)
        {
            this.GetComponent<Dispenser>().Dispense();
        }
    }   
}
