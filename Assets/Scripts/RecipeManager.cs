using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipeManager : MonoBehaviour
{

    //UI object
    public GameObject UIObjectPrefab;
    protected UIOrderQueueManager UIObject;

    //level config, leave blank if random
    [SerializeField]
    public TextAsset levelConfigFile;

    //recipe generation config
    public int totalLevelOrders; //must be > 0
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


            //build new tuple with updated expiry timer and insert into list
            activeOrder = new Tuple<Order, GameObject, int>(activeOrder.Item1, activeOrder.Item2, activeOrder.Item3 - 1);
            activeOrders[i] = activeOrder;

            //if item will expire
            if (activeOrder.Item3 <= 0)
            {
                //delete UI element
                UIObject.deleteOrderUI(activeOrder.Item2);
                
                //apply effects for failing to complete order
                print("oops, you failed to complete the order!");

                //delete order from list
                activeOrders.RemoveAt(i);
                continue;
            }

            //update UI with new time
            //TODO, uncomment once individual gameobjects hold an updateOrderTimer script
            //activeOrder.Item2.updateOrderTimer(activeOrder.Item3);
        }
    }


    //*****************************************************************************
    //prepares the queue of items for the level
    //returns time until first order arrives
    private int generateLevelOrders()
    {
        //randomly generate level data if there is no config
        if (!levelConfigFile)
        {
            int genTime = 0;
            int timeTofirstOrder = -1;
            for (int i = 0; i < totalLevelOrders; i++)
            {
                Order curRecipe = orderPrefabs[UnityEngine.Random.Range(0, orderPrefabs.Length)];
                genTime += UnityEngine.Random.Range(minOrderGap, maxOrderGap);
                //get the time to next order for the first order
                if (timeTofirstOrder == -1)
                {
                    timeTofirstOrder = genTime;
                }

                //get the allowed time for the order
                int orderPrepTime = curRecipe.optimalPrepTime + UnityEngine.Random.Range(minOrderPrepTime, maxOrderPrepTime);

                Tuple<int, Order, int> orderEntry = new Tuple<int, Order, int>(genTime, curRecipe, orderPrepTime);
                queuedOrders.Enqueue(orderEntry);
            }
            return timeTofirstOrder;
        }else
        {
            int timeToFirstOrder = -1;
            //construct level based on config file

            String[] levelData = levelConfigFile.text.Split('\n');
            String[] curOrderData;

            //first line is number of orders
            int nOrders = Convert.ToInt32(levelData[0]);
            print(nOrders);
            //rest of lines are the orders
            for (int i = 1; i < levelData.Length; i++)
            {
                //data for each order is arranged like so: (arrival time, order, prep time)
                //the input data should be ordered by arrival time
                curOrderData = levelData[i].Split(',');
                int tempArrivalTime = Convert.ToInt32(curOrderData[0]);
                Order tempOrderType = orderPrefabs[Convert.ToInt32(curOrderData[1])];
                int tempPrepTime = Convert.ToInt32(curOrderData[2]);
                Tuple<int, Order, int> newOrder = new Tuple<int, Order, int>(tempArrivalTime, tempOrderType, tempPrepTime);
                print(newOrder);
                queuedOrders.Enqueue(newOrder);

                //save the first order arrival time as the initial arrival time
                if (i == 1)
                {
                    timeToFirstOrder = Convert.ToInt32(curOrderData[0]);
                }
            }
            return timeToFirstOrder;
        }
    }



    //start
    void Start()
    {
        UIObject = UIObjectPrefab.GetComponent<UIOrderQueueManager>();
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
                Tuple<int, Order, int> potentialNewOrder = queuedOrders.Peek();

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
                    Tuple<int, Order, int> queuedOrder = queuedOrders.Dequeue();
                    Order newActiveOrder = queuedOrder.Item2;
                    int timeUntilOrderExpiry = queuedOrder.Item3;
                    
                    //create and store a new UI object
                    GameObject newActiveOrderUI = UIObject.addOrderUI(newActiveOrder);

                    //construct the new (active order, ui, expiry time) tuple and add to the active order list
                    Tuple<Order, GameObject, int> newActiveOrderTuple = new Tuple<Order, GameObject, int>(newActiveOrder, newActiveOrderUI, timeUntilOrderExpiry); 
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
