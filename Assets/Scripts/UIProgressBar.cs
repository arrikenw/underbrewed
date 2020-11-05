using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    public GameObject station;

    public float maxTime;

    void Start()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(station.transform.position);
        this.transform.position = new Vector3(pos.x, pos.y + 10, pos.z);
    }
}
