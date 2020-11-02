using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTransitions : MonoBehaviour
{
    public float duration;

    public Camera cam;

    public Transform target;

    public float forwardVelocity = 15.0f;

    public float rotateVelocity = 0.1f;

    public float minDistance = 50.0f;

    //public bool enabled = false;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        StartCoroutine(ZoomCamera());
    }
    /*void FixedUpdate()
    {
        if (enabled)
        {
            Vector3 direction = target.position - transform.position;

            Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, velocity * Time.time);
        }
    }
    
    void LateUpdate()
    {
        if (enabled)
        {

            if (Vector3.Distance(transform.position, target.position) > minDistance)
            {
                camera.fieldOfView -= (Time.deltaTime * velocity);
            }


            Vector3 direction = target.position - transform.position;

            Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, velocity * Time.time);

        }

    }*/

    IEnumerator ZoomCamera()
    {
        float t = 0.0f;

        Vector3 direction = target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (true) 
        {
            t += (Time.deltaTime * rotateVelocity * 0.1f * 0.1f);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);

            if (Vector3.Distance(transform.position, target.position) > minDistance)
            {
                GetComponent<Camera>().fieldOfView -= (Time.deltaTime * forwardVelocity);
            }

            yield return new WaitForEndOfFrame();
        }

    }
}
