using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GameObject PickUpCollider;
    public Rigidbody RigidBody;
    [Range(1.0f, 10.0f)] public float speed = 5.0f; // Camera speed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dx = 0.0f, dz = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            // Move camera forwards
            dz += 1.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            // Move camera backwards
            dz -= 1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // Move camera left
            dx -= 1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            // Move camera right
            dx += 1.0f;
        }


        // get character direction

        if (Input.GetKeyDown(KeyCode.W))
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.forward);
            PickUpCollider.transform.localPosition = transform.forward * 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.back);
            PickUpCollider.transform.localPosition = transform.forward * -1.0f;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.left);
            PickUpCollider.transform.localPosition = transform.right * -1.0f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // GetComponent<Rigidbody>().AddForce(Vector3.right);
            PickUpCollider.transform.localPosition = transform.right * 1.0f;
        }

        transform.position += ((transform.forward * dz) + (transform.right * dx)) * speed * Time.deltaTime;
    }
}
