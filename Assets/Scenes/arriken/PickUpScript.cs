using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour 
{
    private GameObject PickedUp = null;
    private bool IsPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        print("1112222221#");
    }

    // Update is called once per frame
    void LateUpdate()
    {
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
    }

    private void OnTriggerExit(Collider other) 
    {
        print("we left the collision area");
        if (PickedUp == other.gameObject)
        {
            PickedUp = null;
        }
    }





}
