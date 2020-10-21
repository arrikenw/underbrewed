using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    [SerializeField] private int throwMagnitude = 10;
    private GameObject interactable = null;
    private GameObject heldItem = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Throw();
        Interact();
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
            else if (interactable.GetComponent<Station>() != null) {
                heldItem = interactable.GetComponent<Station>().TryPickup();

                if (heldItem != null) {
                    heldItem.GetComponent<Rigidbody>().useGravity = false;

                    // interactable.GetComponent<Station>().OnPickup();
                }
            }
        }

        // NOTE: Commented out because current functionality can be achieved by just dropping the item on
        //       But if we need an actual interact button then that can be coded
        // //interact
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     print("E");
        //     if (interactableTarget != null)
        //     {
        //         if (IsPickedUp == false)
        //         {
        //             print("tried to interact but had empty hands");
        //         }
        //         else
        //         {
        //             print("trying to interact");
        //             print(interactable);
        //             interactable.interact(PickedUp);
        //             //remove from hands
        //             PickedUp = null;
        //             IsPickedUp = false;
        //         }
        //     }
        // }

        // Dropping
        if (Input.GetKeyUp(KeyCode.M) && heldItem != null) {
            print("Dropping");
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            heldItem.GetComponent<Item>().OnDrop();
            heldItem = null;
        }

        // Moving
        if (heldItem != null) {
            heldItem.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
            heldItem.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);
            heldItem.transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));


            // For isKinematic = true...
            // heldItem.transform.position = transform.position + new Vector3(0f, 0.25f, 0f);

            // For useGravity = false...
            heldItem.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(0f, 0.5f, 0f));
        }
    }

    private void Throw() {
        if (Input.GetKeyDown(KeyCode.Period) && heldItem != null) {
            print("Throwing");
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            print(transform.forward);
            heldItem.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * throwMagnitude, ForceMode.Impulse);
            heldItem.GetComponent<Item>().OnDrop();

            heldItem = null;
        }
    }

    private void Interact() {
        if (Input.GetKeyDown(KeyCode.Comma) && interactable != null) {
            print("Interacting");
            interactable.GetComponent<Interactable>().Interact(heldItem);
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
    }

    private void OnTriggerExit(Collider other) 
    {
        if (interactable != null && GameObject.ReferenceEquals(interactable, other.gameObject)) {        
            print("Left interactable");
            interactable.GetComponent<Interactable>().OnLeave();
            interactable = null;
        }
    }
}
