using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipeManager : MonoBehaviour
{

    //UI object
    public GameObject UIObject;


    //recipe generation config
    public int totalLevelOrders;
    public Order[] orderPrefabs;
    public int minOrderGap;
    public int maxOrderGap;
    public int minOrderPrepTime;
    public int maxOrderPrepTime;

    //internal clock logic
    private int score;
    private int curTime;
    private int timeToNextOrder = 0;

    //active and enqueued recipes
    private List<Tuple<Order, GameObject, int>> activeOrders = new List<Tuple<Order, GameObject, int>>(); //(order, ui element, time remaining)
    private Queue<Tuple<int, Order, int>> queuedOrders = new Queue<Tuple<int, Order, int>>(); //(time order will arrive, order, order duration).  note, should added in sorted order by time arrived



    //*****************************************************************************
    //handles the processing of completed potions
    public void processDropoff(Potion potion)
    {
        for (int i = 0; i < activeOrders.Count; i++)
        {
            //check if an active order has destination colour that matches potion
            if (activeOrders[i].Item1.targetColour == potion.potionColour)
            {
                //delete ui
                UIObject.deleteOrderUI(activeOrders[i].Item2);

                //increment score
                score += activeOrders[i].Item1.score;

                //delete order from active list
                activeOrders.RemoveAt(i);
                return;
            }
        }
    }


    //*****************************************************************************
    //handles the tick by tick updates of order lifetime and UI
    private void updateActiveOrders()
    {
        for (int i = 0; i < activeOrders.Count; i++)
        {
            Tuple<Order, GameObject, int> activeOrder = activeOrders[i];

            //update item expiry timer
            activeOrder.Item3 -= 1;

            //if item will expire
            if (activeOrder.Item3 <= 0)
            {
                //delete UI element
                deleteOrderUI(activeOrder.item2);
                
                //apply effects for failing to complete order
                print("oops, you failed to complete the order!");

                //delete order from list
                activeOrders.RemoveAt(i);
                continue;
            }

            //update UI with new time
            activeOrder.Item2.updateOrderTimer(activeOrder.Item3);
        }
    }


    //*****************************************************************************
    //prepares the queue of items for the level
    //returns time until first order arrives
    private int generateLevelOrders()
    {
        int genTime = 0;
        int firstOrderTime = -1;
        for (int i = 0; i < totalLevelOrders; i++)
        {
            Order curRecipe = orderPrefabs[UnityEngine.Random.Range(0, orderPrefabs.Length)];
            genTime += UnityEngine.Random.Range(minOrderGap, maxOrderGap);
            //get the time to next order for the first order
            if (firstOrderTime == -1)
            {
                firstOrderTime = genTime;
            }

            //get the allowed time for the order
            int orderPrepTime = curRecipe.optimalPrepTime + UnityEngine.Random.Range(minOrderPrepTime, maxOrderPrepTime);

            Tuple<int, Order> orderEntry = new Tuple<int, Order>(genTime, curRecipe, orderPrepTime);
            queuedOrders.Enqueue(orderEntry);
        }
    }



    //start
    void Start()
    {
        curTime = 0;
        timeToNextOrder = generateLevelOrders();
    }


    //*****************************************************************************
    //checks to see if we should add new orders from pending queue to active orders
    void Update()
    {
        if (timeToNextOrder <= 0)
        {
            //activate new order
            while (queuedOrders.Count > 0)
            {
                //check next order
                Tuple<int, Order> potentialNewOrder = queuedOrders.Peek();

                //if we reach an order that can't be made yet, stop searching and update time to next order
                if (potentialNewOrder.Item1 > curTime)
                {
                    timeToNextOrder = potentialNewOrder.Item1;
                    break;
                }
                else
                {
                    //if an order has arrived:

                    //remove order from queue
                    Tuple<Order, GameObject, int> queuedOrder = queuedOrders.Dequeue();
                    Order newActiveOrder = queuedOrder.Item2;
                    int timeUntilOrderExpiry = queuedOrder.Item3;
                    
                    //create and store a new UI object
                    GameObject newActiveOrderUI = UIObject.addOrderUI(newActiveOrder);

                    //construct the new (active order, ui, expiry time) tuple and add to the active order list
                    Tuple<Order, GameObject> newActiveOrderTuple = new Tuple<Order, GameObject>(newActiveOrder, newActiveOrderUI, timeUntilOrderExpiry); 
                    activeOrders.Add(newActiveOrderTuple);
                }
            }
        }
        //run lifecycle and ui updates for active orders
        updateActiveOrders();

        //update the current time
        curTime += 1;
        timeToNextOrder -= 1;
    }
}
