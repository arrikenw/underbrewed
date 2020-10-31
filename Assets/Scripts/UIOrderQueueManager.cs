using System.Collections;
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
            case Bone:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Bone");
                methodImage.sprite = null;
            case MeltedBone:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Bone");
                methodImage.sprite = Resources.Load<Sprite>("Asset/Images/Fire");
            case CrushedBone:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Bone");
                methodImage.sprite = Resources.Load<Sprite>("Asset/Images/Mortar_Pestle");
            case Flower:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Lotus");
                methodImage.sprite = null;
            case CharredFlower:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Lotus");
                methodImage.sprite = Resources.Load<Sprite>("Asset/Images/Fire");
            case Cheese:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Cheese");
                methodImage.sprite = null;
            case ChoppedCheese:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Cheese");
                methodImage.sprite = Resrouces.Load<Sprite>("Asset/Images/Knife");
            case Eyeball:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Eyeball");
                methodImage.sprite = null;
            case Eyeball:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Eyeball");
                methodImage.sprite = Resources.Load<Sprite>("Asset/Images/Mortar_Pestle");
            case Frog:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Frog");
                methodImage.sprite = null;
            case ChoppedFrog:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Frog");
                methodImage.sprite = Resrouces.Load<Sprite>("Asset/Images/Knife");
            case CookedFrog:
                ingredientImage.sprite = Resources.Load<Sprite>("Asset/Images/Frog");
                methodImage.sprite = Resrouces.Load<Sprite>("Asset/Images/Fire");
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

            yield return null;
        }
    }
}
