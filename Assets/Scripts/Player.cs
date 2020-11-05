﻿using System.Collections;
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

    int framesUntilComplete = 0;

    public Rigidbody rb;

    private int invert = 1;
    private float invertCountdown = 0.0f;
    public void InvertMovement()
    {
        invert = -1;
        invertCountdown = 10.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        PositionUpdates();
        if (Mathf.Abs(rb.position.y - 0.5f) > 0.01)
        {
            rb.position = new Vector3(rb.position.x, 0.5f, rb.position.y);
        }
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
