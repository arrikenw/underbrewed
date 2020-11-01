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
