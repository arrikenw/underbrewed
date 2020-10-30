using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderQueueManager : MonoBehaviour
{

    public GameObject orderTemplate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject addOrderUI(Order order) 
    {
        // Create new instance of template
        GameObject newOrder = GameObject.Instantiate<GameObject>(orderTemplate);
        newOrder.transform.parent = this.transform;


        // Set timer
        GameObject timer = newOrder.transform.Find("OrderTimer").gameObject;
        timer.GetComponent<UIOrderTimer>().maxTime = order.timeLeft;
        timer.GetComponent<UIOrderTimer>().timeRemaining = order.timeLeft;

        // Set image for potion
        Image potionImage = newOrder.transform.Find("Potion").GetComponent<Image>();
        addPotion(potionImage, order.targetColour);

        // Set images for ingredients
        GameObject recipe = newOrder.transform.Find("Recipe").gameObject;

        //WARNING 
        addIngredient(order.ingredients[0], recipe.transform.Find("Ingredient1").gameObject.GetComponent<Image>(), recipe.transform.Find("Method1").gameObject.GetComponent<Image>());
        // TO DO: repeat for additional ingredients

        // TO DO: set position using update function

        // TO DO: best way to store score?
        newOrder.GetComponent<UIOrderController>().score = order.score;

        return newOrder;

    }

    public void deleteOrderUI(GameObject orderUI)
    {
        Destroy(orderUI);

        // TO DO: reset order queue
    }

    public void addPotion(Image potionImage, Color targetColor)
    {
        //change potion image depending on targetColor
        switch(targetColor)
        {
            // TO DO: complete
            /*
            case purple:
                potionImage.sprite = Resources.Load<Sprite>("Asset/Images/potionPurple");
                */
        }
    }

    public void addIngredient(Ingredient ingredient, Image ingredientImage, Image methodImage)
    {
        //add ingredient according to ingredient and ingredient slot
        switch (ingredient)
        {
            // TO DO: complete 
            /*
            case cheese:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Cheese");
                */
        }
    }

}
