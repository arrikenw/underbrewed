using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : Station
{
    public GameObject tutorialController;

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
            if (tutorialController)
            {
                tutorialController.GetComponent<TutorialScript>().OnUseBin();
            }
        }
    }
}
