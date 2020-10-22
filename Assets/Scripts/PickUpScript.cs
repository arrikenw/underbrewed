using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    [SerializeField] private int throwMagnitude = 10;
    private GameObject interactableObject = null;
    private GameObject heldItem = null;
    public Animator animator;
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
        if (Input.GetKeyDown(KeyCode.M) && heldItem == null && interactableObject != null) {
            animator.Play("PickUp");
            // Pickup item directly
            if (interactableObject.GetComponent<Item>() != null) {
                heldItem = interactableObject;
                heldItem.GetComponent<Rigidbody>().useGravity = false;

                heldItem.GetComponent<Item>().OnPickup();
                interactableObject = null;
            } 

            // Pickup item from station
            else if (interactableObject.GetComponent<Station>() != null) {
                heldItem = interactableObject.GetComponent<Station>().TryPickup();

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
            animator.Play("PutDown");
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
            animator.Play("PutDown");
            print("Throwing");
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            print(transform.forward);
            heldItem.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * throwMagnitude, ForceMode.Impulse);
            heldItem.GetComponent<Item>().OnDrop();

            heldItem = null;
        }
    }

    private void Interact() {
        if (Input.GetKeyDown(KeyCode.Comma) && interactableObject != null) {
            print("Interacting");
            interactableObject.GetComponent<Interactable>().Interact(heldItem);
        }
     }

    private void OnTriggerStay(Collider other)
    {
        if (GameObject.ReferenceEquals(heldItem, other.gameObject)) {
            return;
        }
        
        Interactable interactable = other.gameObject.GetComponent<Interactable>();

        if (interactableObject == null && interactable != null && !interactable.IsLocked()) {
            print("Contacted interactable");
            interactableObject = other.gameObject;
            interactableObject.GetComponent<Interactable>().OnContact();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (interactableObject != null && GameObject.ReferenceEquals(interactableObject, other.gameObject)) {        
            print("Left interactable");
            interactableObject.GetComponent<Interactable>().OnLeave();
            interactableObject = null;
        }
    }
}
