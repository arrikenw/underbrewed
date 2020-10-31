﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderQueueManager : MonoBehaviour
{

    public GameObject orderTemplate;

    public float gutterSize;
    public float velocity;

    public GameObject addOrderUI(Order order) 
    {
        // Create new instance of template
        GameObject newOrder = GameObject.Instantiate<GameObject>(orderTemplate);
        newOrder.transform.parent = this.transform;
        newOrder.transform.localPosition = new Vector3(0, 0, 0);


        // Set timer
        GameObject timer = newOrder.transform.Find("OrderTimer").gameObject;
        timer.GetComponent<UIOrderTimer>().maxTime = order.timeLeft;
        timer.GetComponent<UIOrderTimer>().timeRemaining = order.timeLeft;

        // Set image for potion
        Image potionImage = newOrder.transform.Find("Potion").GetComponent<Image>();
        addPotion(potionImage, order.targetColour);

        // Set images for ingredients
        GameObject recipe = newOrder.transform.Find("Recipe").gameObject;

        addIngredient(order.ingredients[0], recipe.transform.Find("Ingredient1").gameObject.GetComponent<Image>(), recipe.transform.Find("Method1").gameObject.GetComponent<Image>());
        // TO DO: repeat for additional ingredients

        reorderQueue(this.gameObject);

        newOrder.GetComponent<UIOrderController>().score = order.score;

        return newOrder;

    }

    public void deleteOrderUI(GameObject orderUI)
    {
        Destroy(orderUI);

        reorderQueue(this.gameObject);
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

    public void reorderQueue(GameObject orderQueueUI)
    {
        int n = orderQueueUI.transform.childCount;

        for (int i=0; i < n; i++)
        {
            GameObject orderUI = orderQueueUI.transform.GetChild(i).gameObject;

            var rt = orderUI.GetComponent<RectTransform>();
            float width = rt.rect.width;

            Vector3 targetPosition = new Vector3(orderQueueUI.transform.position.x + ((width + this.gutterSize) * i), orderQueueUI.transform.position.y, orderQueueUI.transform.position.z); 

            if (orderUI.transform.position != targetPosition)
            {
                StartCoroutine(animateQueue(orderUI, targetPosition));
            }

        }

    }

    IEnumerator animateQueue(GameObject orderUI, Vector3 targetPosition)
    {
        while (true)
        {

            orderUI.transform.position = Vector3.Lerp(orderUI.transform.position, targetPosition, Time.deltaTime * this.velocity);

            yield return new WaitForSeconds(.1f);
        }
    }
}
