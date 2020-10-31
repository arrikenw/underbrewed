using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Station
{
    // TODO: Connect with game/score manager

    // GameManager Object
    [SerializeField] GameObject gameManager = null;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Deliver();
    }

    private void Deliver() {
        // TEMP CODE
        if (base.storedItem != null) {
            Destroy(base.storedItem);
        }

        // ACTUAL CODE
        // if (base.storedItem != null) {
        //     gameManager.method();
        // }
    }
}
