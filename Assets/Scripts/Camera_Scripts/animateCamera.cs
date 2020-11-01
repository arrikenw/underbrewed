using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateCamera : MonoBehaviour
{
    public GameObject Player;
    public float speed = 0.0000001f;
    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            started = true;
        }
        if(started)
        {
            while (transform.position.x < Player.transform.position.x - 2.435)
            {
                transform.position += new Vector3(1.0f, 0.0f, 0.0f) * speed * Time.deltaTime;
            }

            while (transform.position.x > Player.transform.position.x - 2.435)
            {
                transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * speed * Time.deltaTime;
            }

            while (transform.position.y < Player.transform.position.y + 0.95)
            {
                transform.position += new Vector3(0.0f, 1.0f, 0.0f) * speed * Time.deltaTime;
            }

            while (transform.position.y > Player.transform.position.y + 0.95)
            {
                transform.position += new Vector3(0.0f, -1.0f, 0.0f) * speed * Time.deltaTime;
            }

            while (transform.position.z < Player.transform.position.z - 2.128)
            {
                transform.position += new Vector3(0.0f, 0.0f, 1.0f) * speed * Time.deltaTime;
            }

            while (transform.position.z > Player.transform.position.z - 2.128)
            {
                transform.position += new Vector3(0.0f, 0.0f, -1.0f) * speed * Time.deltaTime;
            }

            // transform.LookAt(Player.transform);
        }
    }
}
