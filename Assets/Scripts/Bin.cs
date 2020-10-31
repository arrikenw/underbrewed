using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : Station
{
    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Dump();
    }

    private void Dump() {
        if (base.storedItem != null) {
            Destroy(base.storedItem);
        }
    }
}
