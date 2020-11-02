using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateCamera : MonoBehaviour
{
    public GameObject Player;
    public float speed = 0.0000001f;
    private bool started = false;
    private int negateX = 1;
    private int negateZ = 1;
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
        if (Player.transform.position.x < 0.0f)
        {
            negateX = -1;
        }
        if (Player.transform.position.z < 0.0f)
        {
            negateZ = -1;
        }
        xSpeed = Mathf.Abs(transform.position.x - (Player.transform.position.x - (2.435f*negateX))) * speed;
        ySpeed = Mathf.Abs(transform.position.y - (Player.transform.position.y + 1.5f)) * speed;
        zSpeed = Mathf.Abs(transform.position.z - (Player.transform.position.z - (2.128f*negateZ))) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            if (transform.position.x < Player.transform.position.x - (2.435*negateX))
            {
                transform.position += new Vector3(1.0f, 0.0f, 0.0f) * xSpeed * Time.unscaledDeltaTime;
            }

            if (transform.position.x > Player.transform.position.x - (2.435*negateX))
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * xSpeed * Time.unscaledDeltaTime;
            }

            if (transform.position.y < Player.transform.position.y + 1.5)
            {
                transform.position += new Vector3(0.0f, 1.0f, 0.0f) * ySpeed * Time.unscaledDeltaTime;
            }

            if (transform.position.y > Player.transform.position.y + 1.5)
            {
                transform.position += new Vector3(0.0f, -1.0f, 0.0f) * ySpeed * Time.unscaledDeltaTime;
            }

            if (transform.position.z < Player.transform.position.z - (2.128*negateZ))
            {
                transform.position += new Vector3(0.0f, 0.0f, 1.0f) * zSpeed * Time.unscaledDeltaTime;
            }

            if (transform.position.z > Player.transform.position.z - (2.128*negateZ))
            {
                transform.position += new Vector3(0.0f, 0.0f, -1.0f) * zSpeed * Time.unscaledDeltaTime;
            }

            transform.LookAt(Player.transform);
        }
    }
}
