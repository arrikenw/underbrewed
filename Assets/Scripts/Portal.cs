using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Station
{
    [SerializeField] GameObject recipeManager = null;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Deliver();
    }

    protected override void OnTriggerStay(Collider other)
    {
        Item potionComponent = other.gameObject.GetComponent<Potion>();
        if (storedItem == null && potionComponent != null && !potionComponent.IsHeld() && !potionComponent.IsLocked()) {
            
            storedItem = other.gameObject;
        }
    }

    public override bool TryDirectStore(Item item)
    {
        if (storedItem == null && item.gameObject.GetComponent<Potion>()) {
            storedItem = item.gameObject;
            return true;
        } else {
            return false; 
        }
    }

    private void Deliver() {
        // ACTUAL CODE
        if (base.storedItem) {

            Potion potion = base.storedItem.GetComponent<Potion>();
            if (potion) {
                recipeManager.GetComponent<RecipeManager>().ProcessDropoff(potion);
            } else {
                print("NOT A POTION");
                // TODO: ERROR to indicate not a potion
            }

            Destroy(base.storedItem);
        }
    }
}
