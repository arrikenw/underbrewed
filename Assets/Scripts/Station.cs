using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;





public class Station : Interactable
{
    // storage logic
    protected GameObject storedItem;
    protected bool canPickup = true;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    /* // Update is called once per frame
     public void Update()
     {

     }
     */

    void OnTriggerStay(Collider other) {
        Item itemComponent = other.gameObject.GetComponent<Item>();
        if (storedItem == null && itemComponent != null && !itemComponent.IsHeld() && !itemComponent.IsLocked()) {
            print("Storing item in Station");
            storedItem = other.gameObject;

            // Set kinematic to true
            storedItem.GetComponent<Rigidbody>().isKinematic = true;

            // Set new position of the stored item
            storedItem.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);

            // Set item is locked and cannot be directly interacted with
            storedItem.GetComponent<Item>().OnStore();
            
            // Set item can be picked up via the station
            canPickup = true;
        }
    }

    // Commented below out because Items shouldn't be leaving on their own anyway

    // void OnTriggerExit(Collider other) {
    //     if (storedItem != null && GameObject.ReferenceEquals(storedItem, other.gameObject)) {        
    //         print("Stored item moved from Station");
    //         storedItem.GetComponent<Rigidbody>().isKinematic = false;
    //         storedItem = null;
    //     }
    // }

    public virtual GameObject TryPickup() {
        if (storedItem != null && canPickup) {
            print("Picking up from Station");
            
            GameObject tempItem = storedItem;
            storedItem = null;

            tempItem.GetComponent<Rigidbody>().isKinematic = false;
            tempItem.GetComponent<Item>().OnPickup();

            return tempItem;
        } else {
            return null;
        }
    }
}
