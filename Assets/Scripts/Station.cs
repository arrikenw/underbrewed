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
        if (storedItem == null && other.gameObject.GetComponent<Item>() != null && !other.gameObject.GetComponent<Item>().IsHeld()) {
            print("Storing item in Station");
            storedItem = other.gameObject;
            storedItem.GetComponent<Rigidbody>().isKinematic = true;
            storedItem.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);
            canPickup = true;
        }
    }

    // void OnTriggerExit(Collider other) {
    //     if (storedItem != null && GameObject.ReferenceEquals(storedItem, other.gameObject)) {        
    //         print("Stored item moved from Station");
    //         storedItem.GetComponent<Rigidbody>().isKinematic = false;
    //         storedItem = null;
    //     }
    // }

    public void OnPickup() {
        storedItem.GetComponent<Rigidbody>().isKinematic = false;
        storedItem.GetComponent<Item>().OnPickup();
        storedItem = null;
    }

    public virtual GameObject TryPickup() {
        if (storedItem != null && canPickup) {
            GameObject tempItem = storedItem;
            OnPickup();
            return tempItem;
        } else {
            return null;
        }
    }
}
