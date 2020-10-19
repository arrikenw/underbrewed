using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    [SerializeField] private int throwMagnitude = 10;
    private GameObject interactable = null;
    private GameObject heldItem = null;
    // private bool hasItem = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Throw();
        Pickup();
    }

    private void Pickup() {

        // Picking Up
        if (Input.GetKeyDown(KeyCode.M) && heldItem == null && interactable != null) {
            
            // Pickup item directly
            if (interactable.GetComponent<Item>() != null) {
                heldItem = interactable;
                heldItem.GetComponent<Rigidbody>().useGravity = false;

                heldItem.GetComponent<Item>().OnPickup();
                interactable = null;
            } 

            // Pickup item from station
            else if (interactable.GetComponent<Station>() != null && interactable.GetComponent<Station>().storedItem != null) {
                heldItem = interactable.GetComponent<Station>().storedItem;

                interactable.GetComponent<Station>().OnPickup();

                heldItem.GetComponent<Rigidbody>().useGravity = false;
            }


        }


        // Dropping
        if (Input.GetKeyUp(KeyCode.M) && heldItem != null) {
            print("dropping");
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            heldItem.GetComponent<Item>().OnDrop();
            heldItem = null;
        }

        // Moving
        if (heldItem != null) {
            heldItem.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
            heldItem.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);
            heldItem.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));


            // For isKinematic = true
            // heldItem.transform.position = transform.position + new Vector3(0f, 0.25f, 0f);

            // For useGravity = false
            heldItem.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(0f, 0.5f, 0f));
        }
    }

    private void Throw() {
        if (Input.GetKeyDown(KeyCode.Period) && heldItem != null) {
            print("throwing");
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            print(transform.forward);
            heldItem.GetComponent<Rigidbody>().AddForce(transform.forward * throwMagnitude, ForceMode.Impulse);
            heldItem.GetComponent<Item>().OnDrop();

            heldItem = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameObject.ReferenceEquals(heldItem, other.gameObject)) {
            return;
        }

        if (interactable == null && other.gameObject.GetComponent<Interactable>() != null) {
            print("Contacted interactable");
            interactable = other.gameObject;
            interactable.GetComponent<Interactable>().OnContact();
        }

        // Interactable item = other.gameObject.GetComponent<Interactable>();
        // if (item != null)
        // {
        //     print("Interactable found");
        // }
  
        // if (item != null && PickedUp == null && IsPickedUp)
        // {
        //     PickedUp = other.gameObject;
        // }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (interactable != null && GameObject.ReferenceEquals(interactable, other.gameObject)) {        
            print("Left interactable");
            interactable.GetComponent<Interactable>().OnLeave();
            interactable = null;
        }
        // print("we left the collision area");
        // if (PickedUp == other.gameObject)
        // {
        //     PickedUp = null;
        // }
    }





}
