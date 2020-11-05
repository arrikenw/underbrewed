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

    protected override void OnTriggerStay(Collider other)
    {
        Item itemComponent = other.gameObject.GetComponent<Item>();
        if (storedItem == null && itemComponent != null && !itemComponent.IsHeld() && !itemComponent.IsLocked()) {
            
            storedItem = other.gameObject;
        }
    }

    public override bool TryDirectStore(Item item)
    {
        if (storedItem == null) {
            storedItem = item.gameObject;
            return true;
        } else {
            return false; 
        }
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
