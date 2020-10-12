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

public class character : MonoBehaviour
{
    public state currentState = state.Idle;
    float maxSpeed = 0.04f;
    float speedDecay = 0.98f;
    Vector3 speedVector = new Vector3(0, 0, 0);

    int framesUntilComplete = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        positionUpdates();
        actionUpdates();
    }

    void actionUpdates()
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

    void positionUpdates()
    {
        //TODO MAYBE REWRITE TO USE ACCEL, VELOCITY ETC.
        //GetKey rather than GetKeyDown to ensure that holding does more than a tap
        Vector3 updateSpeedVector = new Vector3(0, 0, 0);

        if (currentState != state.Stunned)
        {
            if (Input.GetKey("d"))
            {
                updateSpeedVector = updateSpeedVector + new Vector3(1, 0, 0);
            }
            if (Input.GetKey("s"))
            {
                updateSpeedVector = updateSpeedVector + new Vector3(0, 0, -1);
            }
            if (Input.GetKey("a"))
            {
                updateSpeedVector = updateSpeedVector + new Vector3(-1, 0, 0);
            }
            if (Input.GetKey("w"))
            {
                updateSpeedVector = updateSpeedVector + new Vector3(0, 0, 1);
            }
        }

        //normalize so that the character has the same speed on diagonals
        updateSpeedVector.Normalize();
        updateSpeedVector = updateSpeedVector / 30;

        speedVector = speedVector * speedDecay + updateSpeedVector;

        if (speedVector.magnitude > maxSpeed)
        {
            speedVector.Normalize();
            speedVector = speedVector * maxSpeed;
        }

        //print("magnitude " + speedVector.magnitude);
        transform.Translate(speedVector, Space.World);
        //second vector is to orient the hat so that it faces upwards
        transform.rotation = Quaternion.LookRotation(speedVector + new Vector3(0, 0, 0));
    }
}
