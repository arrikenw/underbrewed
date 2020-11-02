using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateCamera : MonoBehaviour
{
    public GameObject Player;
    public float speed = 0.0000001f;
    private bool started = false;
    private float xSpeed;
    private float ySpeed;
    private float zSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EndAnimation()
    {
        started = true;
        xSpeed = Mathf.Abs(transform.position.x - (Player.transform.position.x - 2.435f)) * speed;
        ySpeed = Mathf.Abs(transform.position.y - (Player.transform.position.y + 0.95f)) * speed;
        zSpeed = Mathf.Abs(transform.position.z - (Player.transform.position.z - 2.128f)) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            if (transform.position.x < Player.transform.position.x - 2.435)
            {
                transform.position += new Vector3(1.0f, 0.0f, 0.0f) * xSpeed * Time.deltaTime;
            }

            if (transform.position.x > Player.transform.position.x - 2.435)
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * xSpeed * Time.deltaTime;
            }

            if (transform.position.y < Player.transform.position.y + 0.95)
            {
                transform.position += new Vector3(0.0f, 1.0f, 0.0f) * ySpeed * Time.deltaTime;
            }

            if (transform.position.y > Player.transform.position.y + 0.95)
            {
                transform.position += new Vector3(0.0f, -1.0f, 0.0f) * ySpeed * Time.deltaTime;
            }

            if (transform.position.z < Player.transform.position.z - 2.128)
            {
                transform.position += new Vector3(0.0f, 0.0f, 1.0f) * zSpeed * Time.deltaTime;
            }

            if (transform.position.z > Player.transform.position.z - 2.128)
            {
                transform.position += new Vector3(0.0f, 0.0f, -1.0f) * zSpeed * Time.deltaTime;
            }

            transform.LookAt(Player.transform);
        }
    }
}
