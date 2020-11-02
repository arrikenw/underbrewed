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
    }

    void Update()
    {
        ActionUpdates();
        if (invertCountdown <= 0.0f)
        {
            invert = 1;
        }
        else
        {
            invertCountdown -= Time.deltaTime;
        }
    }

    void ActionUpdates()
    {

        //finish action and return to idle, or reduce number of frames left in action
        if (framesUntilComplete <= 0)
        {
            if (currentState != state.Grabbing && currentState != state.Idle && currentState != state.Holding)
            {
                print("RETURNED TO IDLE STATE");
                currentState = state.Idle;
            }
            if (currentState == state.Grabbing)
            {
                print("GRAB FINISHED, NOW IN HOLDING STATE");
                currentState = state.Holding;
            }

        }
        else
        {
            //print("FRAMES LEFT " + framesUntilComplete);
            framesUntilComplete = framesUntilComplete - 1;
        }

        //can't perform actions when stunned
        if (currentState == state.Stunned)
        {
            return;
        }

        if (currentState == state.Idle)
        {
            //placeholder for testing. I think grab should be performed when the player interacts with a holdable object.
            if (Input.GetKeyDown("f"))
            {
                //startGrab()
                print("STARTED GRABBING");
                currentState = state.Grabbing;
                framesUntilComplete = 30;
            }

            if (Input.GetKeyDown("space"))
            {
                //startInteract()
                print("STARTED INTERACTING");
                currentState = state.Interacting;
                framesUntilComplete = 60;
            }
        }

        if (currentState == state.Holding)
        {
            print("HOLDING RN");
            //TODO change animations etc.
            if (Input.GetKeyDown("e"))
            {
                //startThrow()
                print("STARTED THROWING");
                currentState = state.Throwing;
                framesUntilComplete = 20;
            }

            if (Input.GetKeyDown("q"))
            {
                //startDrop()
                print("STARTED DROPPING");
                currentState = state.Dropping;
                framesUntilComplete = 30;
            }
        }


        if (currentState == state.Throwing)
        {
            //TODO
        }
        if (currentState == state.Dropping)
        {
            //TODO
        }
        if (currentState == state.Interacting)
        {
            //TODO
        }

    }

    void PositionUpdates()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = (new Vector3(h*invert, 0.0f, v* invert)).normalized;

        rb.MovePosition(rb.position + direction * speed);
        rb.velocity = new Vector3(0f,0f,0f); 
        rb.angularVelocity = new Vector3(0f,0f,0f);
        if (direction == new Vector3(0.0f, 0.0f, 0.0f)) {
            rb.MoveRotation(Quaternion.LookRotation(transform.forward));
        } else {
            rb.MoveRotation(Quaternion.LookRotation(direction));
        }




        // //TODO MAYBE REWRITE TO USE ACCEL, VELOCITY ETC.
        // //GetKey rather than GetKeyDown to ensure that holding does more than a tap
        // Vector3 updateSpeedVector = new Vector3(0, 0, 0);

        // if (currentState != state.Stunned)
        // {
        //     if (Input.GetKey("d"))
        //     {
        //         updateSpeedVector = updateSpeedVector + new Vector3(1, 0, 0);
        //     }
        //     if (Input.GetKey("s"))
        //     {
        //         updateSpeedVector = updateSpeedVector + new Vector3(0, 0, -1);
        //     }
        //     if (Input.GetKey("a"))
        //     {
        //         updateSpeedVector = updateSpeedVector + new Vector3(-1, 0, 0);
        //     }
        //     if (Input.GetKey("w"))
        //     {
        //         updateSpeedVector = updateSpeedVector + new Vector3(0, 0, 1);
        //     }
        // }

        // //normalize so that the character has the same speed on diagonals
        // updateSpeedVector.Normalize();
        // updateSpeedVector = updateSpeedVector / 30;

        // speedVector = speedVector * speedDecay + updateSpeedVector;

        // if (speedVector.magnitude > maxSpeed)
        // {
        //     speedVector.Normalize();
        //     speedVector = speedVector * maxSpeed;
        // }

        // //print("magnitude " + speedVector.magnitude);
        // transform.Translate(speedVector, Space.World);
        // //second vector is to orient the hat so that it faces upwards
        // transform.rotation = Quaternion.LookRotation(speedVector + new Vector3(0, 0, 0));
    }
}
