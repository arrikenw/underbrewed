using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTORIALMANAGERSCRIPT : MonoBehaviour
{
    public GameObject UIObject;
    public GameObject MRCAULDRON;

    void Update()
    {
        if (UIObject.transform.childCount == 0)
        {
            UIObject.SetActive(false);
            MRCAULDRON.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (UIObject.activeSelf)
            {
                if (UIObject.transform.childCount >= 2)
                {
                    UIObject.transform.GetChild(1).gameObject.SetActive(true);
                }
                Destroy(UIObject.transform.GetChild(0).gameObject);
            }
        }
    }
}
