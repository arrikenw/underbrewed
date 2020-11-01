﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderQueueManager : MonoBehaviour
{
    /*
    public enum IngredientType
    {
        Null,
        Bone,
        MeltedBone,
        CrushedBone,
        Flower,
        CharredFlower,
        Cheese,
        ChoppedCheese,
        Eyeball,
        CrushedEyeball,
        Frog,
        ChoppedFrog,
        CookedFrog
    }
    */

    public GameObject orderTemplate;

    public float gutterSize = 15.0f;
    public float velocity = 4.0f;

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
        /*
        switch (ingredient)
        {
            case Bone: // Bone is a type...
                ingredientImage.sprite = Resources.Load<Sprite>("Bone");
                methodImage.color = new Color (0, 0, 0, 0);
                methodImage.sprite = null;
            case MeltedBone:
                ingredientImage.sprite = Resources.Load<Sprite>("Bone");
                methodImage.sprite = Resources.Load<Sprite>("Fire");
            case CrushedBone:
                ingredientImage.sprite = Resources.Load<Sprite>("Bone");
                methodImage.sprite = Resources.Load<Sprite>("Mortar_Pestle");
            case Flower:
                ingredientImage.sprite = Resources.Load<Sprite>("Lotus");
                methodImage.color = new Color (0, 0, 0, 0);
                methodImage.sprite = null;
            case CharredFlower:
                ingredientImage.sprite = Resources.Load<Sprite>("Lotus");
                methodImage.sprite = Resources.Load<Sprite>("Fire");
            case Cheese:
                ingredientImage.sprite = Resources.Load<Sprite>("Cheese");
                methodImage.color = new Color (0, 0, 0, 0);
                methodImage.sprite = null;
            case ChoppedCheese:
                ingredientImage.sprite = Resources.Load<Sprite>("Cheese");
                methodImage.sprite = Resrouces.Load<Sprite>("Knife");
            case Eyeball:
                ingredientImage.sprite = Resources.Load<Sprite>("Eyeball");
                methodImage.color = new Color (0, 0, 0, 0);        
                methodImage.sprite = null;
            case Eyeball:
                ingredientImage.sprite = Resources.Load<Sprite>("Eyeball");
                methodImage.sprite = Resources.Load<Sprite>("Mortar_Pestle");
            case Frog:
                ingredientImage.sprite = Resources.Load<Sprite>("Frog");
                methodImage.color = new Color (0, 0, 0, 0);
                methodImage.sprite = null;
            case ChoppedFrog:
                ingredientImage.sprite = Resources.Load<Sprite>("Frog");
                methodImage.sprite = Resrouces.Load<Sprite>("Knife");
            case CookedFrog:
                ingredientImage.sprite = Resources.Load<Sprite>("Frog");
                methodImage.sprite = Resrouces.Load<Sprite>("Fire");
        
        }
    */
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

            yield return new WaitForEndOfFrame();
        }
    }
}