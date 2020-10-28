using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : Station
{
    [SerializeField] private GameObject prefabItem = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override GameObject TryPickup() {
        GameObject item = base.TryPickup();

        if (item != null) {
            return item;
        } else {
            return Dispense();
        }
    }

    public GameObject Dispense() {
        GameObject item = Instantiate(prefabItem);
        item.GetComponent<Item>().OnDispense();
        return item;
    }
}
