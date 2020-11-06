using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum state
{
    Holding,
    Grabbing,
    Throwing,
    Stunned,
    Dropping,
    Interacting,
    Idle
}

public enum item
{
    None,
    Mushroom,
    Wand
}

public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 0.075f;
    public state currentState = state.Idle;
    // float maxSpeed = 0.04f;
    // float speedDecay = 0.98f;
    Vector3 speedVector = new Vector3(0, 0, 0);

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        PositionUpdates();
    }


    void PositionUpdates()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = (new Vector3(h, 0.0f, v)).normalized;

        rb.MovePosition(rb.position + direction * speed);
        rb.velocity = new Vector3(0f,0f,0f); 
        rb.angularVelocity = new Vector3(0f,0f,0f);
        if (direction == new Vector3(0.0f, 0.0f, 0.0f)) {
            rb.MoveRotation(Quaternion.LookRotation(transform.forward));
        } else {
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }
    }
}
