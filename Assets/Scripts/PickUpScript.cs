using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    [SerializeField] private int throwMagnitude = 10;
    private GameObject interactableObject = null;
    private GameObject heldItem = null;
    public Animator animator;

    private int pollGap = 10; //testing, smoother than getkeydown, which was sometimes quite rough

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        UpdateThrow();
        UpdateInteract();
        UpdatePickup();

        if (pollGap > 0)
        {
            pollGap -= 1;
        }
    }

    void Update()
    {
        UpdateThrow();
        UpdateInteract();
        UpdatePickup();
    }

    private void UpdatePickup() {
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
                }
            }
        }

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

            // Reset Velocities
            heldItem.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
            heldItem.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);

            // Set Rotation via Transform
            heldItem.transform.rotation = Quaternion.LookRotation(transform.forward);


            // Set Position via Rigidbody

            // For isKinematic = true...
            // heldItem.transform.position = transform.position + new Vector3(0f, 0.25f, 0f);

            // For useGravity = false...
            heldItem.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(0f, 0.5f, 0f));
        }
    }

    private void UpdateThrow() {
        if (Input.GetKeyDown(KeyCode.Period) && heldItem != null) {
            animator.Play("PutDown");
            print("Throwing");
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            heldItem.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * throwMagnitude, ForceMode.Impulse);
            heldItem.GetComponent<Rigidbody>().AddForce(transform.up.normalized * 3, ForceMode.Impulse);
            heldItem.GetComponent<Item>().OnDrop();

            heldItem = null;
        }
    }

    private void UpdateInteract() {

        //dealing with other interactables
        if (Input.GetKeyDown(KeyCode.Comma) && interactableObject != null) {
            //we want to interact with interactable processors differently, so return 
            if (!interactableObject.GetComponent<Processor>()) {
                print("Interacting");
                interactableObject.GetComponent<Interactable>().Interact(heldItem);
            }
        }

        //dealing with processors
        if (interactableObject != null)
        {
            //processor
            Processor processor = interactableObject.GetComponent<Processor>();
            bool isHoldProcessor = !(interactableObject.GetComponent<HoldProcessor>() == null);

            if (processor != null && pollGap == 0 && Input.GetKey(KeyCode.Comma))
            {
                print("Processing!");
                //toggling on with G works for both
                if (!processor.getInteract())
                {
                    processor.AttemptStartInteract();
                }else
                {
                    //toggling off with G is enabled only for togglable processors
                    if (!isHoldProcessor)
                    {
                        processor.AttemptStopInteract();
                    }
                }
                pollGap = 10;
            }

            //instantly stop, ignoring polling gap. Gives a smoother feel
            if (processor != null && !Input.GetKey(KeyCode.Comma))
            {
                if (isHoldProcessor)
                {
                    processor.AttemptStopInteract();
                }
            }
        }
     }

    private void OnTriggerStay(Collider other)
    {
        if (GameObject.ReferenceEquals(heldItem, other.gameObject)) {
            return;
        }

        
        /*
        //change our focus, so stop interacting
        if (interactableObject)
        {
            HoldProcessor holdProcessor = interactableObject.GetComponent<HoldProcessor>();
            if (holdProcessor)
            {
                holdProcessor.AttemptStopInteract();
            }
        }
        */

        Interactable interactable = other.gameObject.GetComponent<Interactable>();

        if (interactableObject == null && interactable != null && !interactable.IsLocked()) {
            print("Contacted interactable");
            interactableObject = other.gameObject;
            interactableObject.GetComponent<Interactable>().OnContact();
        }
    }

    private void OnTriggerExit(Collider other) 
    {

        //stop interaction on leave
        if (interactableObject != null)
        {
            HoldProcessor holdProcessor = interactableObject.GetComponent<HoldProcessor>();
            if (holdProcessor != null)
            {
                holdProcessor.AttemptStopInteract();
            }
        }



        if (interactableObject != null && GameObject.ReferenceEquals(interactableObject, other.gameObject)) {        
            print("Left interactable");
            interactableObject.GetComponent<Interactable>().OnLeave();
            interactableObject = null;
        }
    }
}
