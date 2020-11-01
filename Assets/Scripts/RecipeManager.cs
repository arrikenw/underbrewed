using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipeManager : MonoBehaviour
{

    //UI object
    public GameObject MainUIObject;
    protected UIOrderQueueManager UIObject;
    

    //testing score
    public GameObject ScoreObject;
    public GameObject TimeObject;

    //level config, leave blank if random
    [SerializeField]
    public TextAsset levelConfigFile;

    //recipe generation config
    public int totalLevelOrders; //must be > 0
    public Order[] orderPrefabs;
    public float minOrderGap;
    public float maxOrderGap;
    public float minOrderPrepTime;
    public float maxOrderPrepTime;

    //internal clock logic
    private int score;
    private float curTime;
    private float timeToNextOrder = 0;

    //active and enqueued recipes
    private List<Tuple<Order, GameObject, float>> activeOrders = new List<Tuple<Order, GameObject, float>>(); //(order, ui element, time remaining)
    private Queue<Tuple<float, Order, float>> queuedOrders = new Queue<Tuple<float, Order, float>>(); //(time order will arrive, order, order duration).  note, should added in sorted order by time arrived


    //*****************************************************************************
    //handles the processing of completed potions
    public void ProcessDropoff(Potion potion)
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
    private void UpdateActiveOrders()
    {
        for (int i = 0; i < activeOrders.Count; i++)
        {
            Tuple<Order, GameObject, float> activeOrder = activeOrders[i];


            //build new tuple with updated expiry timer and insert into list
            activeOrder = new Tuple<Order, GameObject, float>(activeOrder.Item1, activeOrder.Item2, activeOrder.Item3 - Time.deltaTime);
            activeOrders[i] = activeOrder;

            //if item will expire
            if (activeOrder.Item3 <= 0.0f)
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
            activeOrder.Item2.GetComponent<UIOrderController>().updateOrderTimer(activeOrder.Item3);
        }
    }


    //*****************************************************************************
    //prepares the queue of items for the level
    //returns time until first order arrives
    private float GenerateLevelOrders()
    {
        float timeTofirstOrder = -1.0f;
        //randomly generate level data if there is no config
        if (!levelConfigFile)
        {
            float genTime = 0.0f;
            for (int i = 0; i < totalLevelOrders; i++)
            {
                Order curRecipe = orderPrefabs[UnityEngine.Random.Range(0, orderPrefabs.Length)];
                genTime += UnityEngine.Random.Range(minOrderGap, maxOrderGap);
                //get the time to next order for the first order
                if (timeTofirstOrder == -1.0f)
                {
                    timeTofirstOrder = genTime;
                }

                //get the allowed time for the order
                float orderPrepTime = curRecipe.optimalPrepTime + UnityEngine.Random.Range(minOrderPrepTime, maxOrderPrepTime);

                Tuple<float, Order, float> orderEntry = new Tuple<float, Order, float>(genTime, curRecipe, orderPrepTime);
                queuedOrders.Enqueue(orderEntry);
            }
            return timeTofirstOrder;
        }else
        {
            float timeToFirstOrder = -1.0f;
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
                float tempArrivalTime = Convert.ToSingle(curOrderData[0]);
                Order tempOrderType = orderPrefabs[Convert.ToInt32(curOrderData[1])];
                float tempPrepTime = Convert.ToSingle(curOrderData[2]);
                Tuple<float, Order, float> newOrder = new Tuple<float, Order, float>(tempArrivalTime, tempOrderType, tempPrepTime);
                print(newOrder);
                queuedOrders.Enqueue(newOrder);

                //save the first order arrival time as the initial arrival time
                if (i == 1)
                {
                    timeToFirstOrder = Convert.ToSingle(curOrderData[0]);
                }
            }
            return timeToFirstOrder;
        }
    }



    //start
    void Start()
    {
        UIObject = MainUIObject.GetComponent<UIOrderQueueManager>();
        curTime = 0.0f;
        timeToNextOrder = GenerateLevelOrders();

        //testing score
        ScoreObject.GetComponent<UIGameScore>().updateGameScore(500);

        //testing timer
        TimeObject.GetComponent<UIGameTimer>().updateGameTimer(999);

    }


    //*****************************************************************************
    //checks to see if we should add new orders from pending queue to active orders
    void Update()
    {
            //activate new order
            while (queuedOrders.Count > 0)
            {
                //check next order
                Tuple<float, Order, float> potentialNewOrder = queuedOrders.Peek();

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
                    Tuple<float, Order, float> queuedOrder = queuedOrders.Dequeue();
                    Order newActiveOrder = queuedOrder.Item2;
                    float timeUntilOrderExpiry = queuedOrder.Item3;

                    //create and store a new UI object
                    GameObject newActiveOrderUI = UIObject.addOrderUI(newActiveOrder, timeUntilOrderExpiry);

                    //construct the new (active order, ui, expiry time) tuple and add to the active order list
                    Tuple<Order, GameObject, float> newActiveOrderTuple = new Tuple<Order, GameObject, float>(newActiveOrder, newActiveOrderUI, timeUntilOrderExpiry); 
                    activeOrders.Add(newActiveOrderTuple);
                }
            }

        //run lifecycle and ui updates for active orders
        UpdateActiveOrders();

        //update the current time
        curTime += Time.deltaTime;
    }
}
