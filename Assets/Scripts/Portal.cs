using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Station
{
    // TODO: Connect with game/score manager

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Deliver();
    }

    private void Deliver() {
        if (base.storedItem != null) {
            Destroy(base.storedItem);
        }
    }
}
