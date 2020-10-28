using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderQueueManager : MonoBehaviour
{

    public GameObject orderTemplate;


    // TO DO: find Order type... lol...
    public Order order;
    public GameObject orderUI;

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
        GameObject timer = newOrder.transform.Find("RecipeTimer").gameObject;
        timer.GetComponent<UIRecipeTimer>().maxTime = order.timeLeft;
        timer.GetComponent<UIRecipeTimer>().timeRemaining = order.timeLeft;

        // Set image for potion
        Component potionImage = newOrder.transform.Find("Potion").GetComponent<Image>();
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

    public void updateOrderTimer(Order order)
    {
        // TO DO: link order to order UI object somehow ... should Order include a GameObject for the OrderUI? 
    }

    public void addPotion(Component potionImage, Color targetColor)
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

    public void addIngredient(Ingredient ingredient, Component ingredientImage, Component methodImage)
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
