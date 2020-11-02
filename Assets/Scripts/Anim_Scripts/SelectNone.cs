using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNone : StateMachineBehaviour
{
    public GameObject knife;
    public GameObject pestle;
    // Start is called before the first frame update
    void OnStateEnter()
    {
        knife.SetActive(false);
        pestle.SetActive(false);
    }

}
