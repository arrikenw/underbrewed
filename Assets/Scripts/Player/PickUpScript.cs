using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    [SerializeField] private int throwMagnitude = 10;
    private GameObject interactableObject = null;
    private GameObject heldItem = null;
    public Animator animator;
    public AudioSource pickUpSound;
    public AudioSource putDownSound;

    //tutorial
    public GameObject tutorialController;
    private int tutorialCount = 3;


    void Update()
    {
        UpdateThrow();
        UpdateInteract();
        UpdatePickup();
    }

    private void UpdatePickup() {
        // Picking Up and dropping 
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Z)) 
        {
            if (heldItem == null && interactableObject != null)
            {
                // Pickup item directly
                if (interactableObject.GetComponent<Item>() != null)
                {
                    animator.Play("PickUp");
                    pickUpSound.Play();
                    heldItem = interactableObject;
                    // heldItem.GetComponent<Rigidbody>().useGravity = false;
                    heldItem.GetComponent<Rigidbody>().isKinematic = true;
                    heldItem.GetComponent<Collider>().enabled = false;

                    heldItem.GetComponent<Item>().OnPickup();
                    interactableObject = null;
                }

                // Pickup item from station
                else if (interactableObject.GetComponent<Station>() != null)
                {
                    heldItem = interactableObject.GetComponent<Station>().TryPickup();

                    if (heldItem != null)
                    {
                        animator.Play("PickUp");
                        pickUpSound.Play();
                        // heldItem.GetComponent<Rigidbody>().useGravity = false;
                        heldItem.GetComponent<Rigidbody>().isKinematic = true;
                        heldItem.GetComponent<Collider>().enabled = false;
                    }
                }
            }

            // Dropping
            else if (heldItem != null)
            {
                animator.Play("PutDown");
                // heldItem.GetComponent<Rigidbody>().useGravity = true;
                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.GetComponent<Collider>().enabled = true;
                putDownSound.Play();
                //pickUpSound.Play();

                bool directStore = false;
                // Try storing to a station directly first
                if (interactableObject != null && interactableObject.GetComponent<Station>())
                {
                    directStore = interactableObject.GetComponent<Station>().TryDirectStore(heldItem.GetComponent<Item>());
                }

                if (!directStore)
                {
                    heldItem.GetComponent<Item>().OnDrop();
                }

                heldItem = null;

                //progress to next tutorial message after 3 attempts to grab and drop an object
                if (tutorialController)
                {
                    tutorialCount -= 1;
                    if (tutorialCount <= 0)
                    {
                        tutorialController.GetComponent<TutorialScript>().OnLearnPickup();
                    }
                }
            }
        }


        // Moving
        if (heldItem != null) 
        {

            // Reset Velocities
            heldItem.GetComponent<Rigidbody>().velocity = new Vector3(0f,0f,0f); 
            heldItem.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f,0f,0f);

            // Set Rotation via Transform
            heldItem.transform.rotation = Quaternion.LookRotation(transform.forward);


            // Set Position via Rigidbody

            // For isKinematic = true...
            heldItem.transform.position = transform.position + new Vector3(0f, 0.5f, 0f);

            // For useGravity = false...
            // heldItem.GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(0f, 0.5f, 0f));
        }
    }

    private void UpdateThrow() 
    {
        if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown("[") || Input.GetKeyDown(KeyCode.C)) 
        {
            if (heldItem != null)
            {

                animator.Play("PutDown");
                putDownSound.Play();
                // heldItem.GetComponent<Rigidbody>().useGravity = true;
                heldItem.GetComponent<Rigidbody>().isKinematic = false;
                heldItem.GetComponent<Collider>().enabled = true;
                heldItem.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * throwMagnitude, ForceMode.Impulse);
                heldItem.GetComponent<Rigidbody>().AddForce(transform.up.normalized * 1.5f, ForceMode.Impulse);
                heldItem.GetComponent<Item>().OnDrop();

                heldItem = null;
            }
            
        }
    }

    private void UpdateInteract() 
    {
        Processor processor;

        // only continue if we have interactable object
        if (!interactableObject)
        {
            return;
        }
        else
        {
            processor = interactableObject.GetComponent<Processor>();
        }
        
        //dealing with interacting
        if (Input.GetKeyDown(KeyCode.Comma) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.X)) 
        {
            // non processors get interacted with here
            if (!processor)
            {
                interactableObject.GetComponent<Interactable>().Interact(heldItem);
            }

            // else processors get delt with here
            else
            {
                // if processors has not started, start
                if (processor && !processor.getInteract())
                {
                    processor.AttemptStartInteract();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Comma) || Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.X))
        {
            // if you let go on a holder processor, stop
            if (interactableObject.GetComponent<HoldProcessor>())
            {
                processor.AttemptStopInteract();
            }
        }

            /*//dealing with processors
            if (interactableObject != null)
        {
            //processor
            Processor processor = interactableObject.GetComponent<Processor>();
            bool isHoldProcessor = !(interactableObject.GetComponent<HoldProcessor>() == null);

            if (processor != null && pollGap == 0 && Input.GetKey(KeyCode.Comma))
            {
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
        }*/
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

        if (interactableObject == null && interactable != null && !interactable.IsLocked()) 
        {
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



        if (interactableObject != null && GameObject.ReferenceEquals(interactableObject, other.gameObject)) 
        {        
            interactableObject.GetComponent<Interactable>().OnLeave();
            interactableObject = null;
        }
    }
}
