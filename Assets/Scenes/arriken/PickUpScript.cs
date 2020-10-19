using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    private GameObject PickedUp = null;
    private bool IsPickedUp = false;

    private GameObject interactableTarget = null;
    private Interactable interactable = null;

    // Start is called before the first frame update
    void Start()
    {
        print("1112222221#");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("E");
            if (interactableTarget != null)
            {
                if (IsPickedUp == false)
                {
                    print("tried to interact but had empty hands");
                }
                else
                {
                    print("trying to interact");
                    print(interactable);
                    interactable.interact(PickedUp);
                    //remove from hands
                    PickedUp = null;
                    IsPickedUp = false;
                }
            }
        }

        //pickup
        if (Input.GetKeyDown(KeyCode.M))
        {
            IsPickedUp = true;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            IsPickedUp = false;
        }
        if (PickedUp != null && IsPickedUp == true)
        {
            PickedUp.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Plane")
        {
            print("we are in collision area");
        }
  
        if (other.gameObject.name != "Plane" && PickedUp == null)
        {
            PickedUp = other.gameObject;
        }

        if (other.gameObject.name == "Bench"){
            interactableTarget = other.gameObject;
            interactable = interactableTarget.GetComponent(typeof(Interactable)) as Interactable;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        print("we left the collision area");
        if (PickedUp == other.gameObject)
        {
            PickedUp = null;
        }

        //idk if == works here
        if (interactableTarget == other.gameObject){
            interactableTarget = null;
        }
    }
}
