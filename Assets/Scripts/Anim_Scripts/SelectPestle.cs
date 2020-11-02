using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPestle : StateMachineBehaviour
{
    public GameObject pestle;
    // Start is called before the first frame update
    void OnStateEnter()
    {
        pestle.SetActive(true);
    }

}
