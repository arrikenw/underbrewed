using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAutoMove : MonoBehaviour
{
    [Range(0.0f, 10.0f)] public float speed = 5.0f; // Camera speed
    public float z = 0.0f;
    public float y = 0.0f;
    public float x = 0.0f;
    public float forward = 0.0f;
    public float right = 0.0f;
    public float up = 0.0f;
    public bool disabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            disabled = !disabled;
        }
        if (!disabled)
        {
            transform.position += ((transform.forward * forward) + (transform.right * right) + (transform.up * up)) * speed * Time.deltaTime;
            transform.position += new Vector3(x, y, z) * speed * Time.deltaTime;
        }
    }
}
