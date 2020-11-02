using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectKnife : StateMachineBehaviour
{
    public GameObject knife;
    // Start is called before the first frame update
    void OnStateEnter()
    {
        knife.SetActive(true);
    }

}
