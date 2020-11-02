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

    //random recipe generation config
    public int totalLevelOrders; //must be > 0
    public Order[] orderPrefabs;
    public float minOrderGap;
    public float maxOrderGap;
    public float minOrderPrepTime;
    public float maxOrderPrepTime;

    //internal clock logic
    public float countdownDuration;
    private bool inCountdown;
    private bool levelIsEnded;
    private float curTime;
    private float levelEndTime;
    private float timeToNextOrder = 0;

    //scoring and statistics
    private int nOrdersCompleted;
    private int score;

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

            //commented out for testing
            if (activeOrders[i].Item1.targetColour == potion.potionColour)
            {
                //delete ui
                UIObject.deleteOrderUI(activeOrders[i].Item2);

                //increment score
                score += activeOrders[i].Item1.score;

                //update statistics
                nOrdersCompleted += 1;

                //update score visuals
                ScoreObject.GetComponent<UIGameScore>().updateGameScore(score);

                //delete order from active list
                activeOrders.RemoveAt(i);

                //run endgame logic if the last request was completed and there are no more queued orders
                if (activeOrders.Count == 0 && queuedOrders.Count == 0)
                {
                    endLevel();
                }
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
    //tutorial use only!!! adds a single order to the active orders
    public void tutorialAddExampleOrder()
    {
        Order exampleOrder = orderPrefabs[0];//always just use the first prefab for the example
        Tuple<Order, GameObject, float> newActiveOrderTuple = new Tuple<Order, GameObject, float>(exampleOrder, newActiveOrderUI, timeUntilOrderExpiry);
        activeOrders.Add(newActiveOrderTuple);
    }


    //*****************************************************************************
    //prepares the queue of items for the level
    //returns time until first order arrives
    private float GenerateLevelOrders()
    {
        float timeTofirstOrder = -1.0f;

        float orderPrepTime = 0.0f;
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
                orderPrepTime = curRecipe.optimalPrepTime + UnityEngine.Random.Range(minOrderPrepTime, maxOrderPrepTime);

                Tuple<float, Order, float> orderEntry = new Tuple<float, Order, float>(genTime, curRecipe, orderPrepTime);
                queuedOrders.Enqueue(orderEntry);
            }
            if (totalLevelOrders == 0)
            {
                print("CAN'T GENERATE LEVEL WITH 0 ORDERS");
                return 0;
            }
            //the final order time is the same as the end of the level
            levelEndTime = genTime + orderPrepTime;
            return timeTofirstOrder;
        }else
        {
            float timeToFirstOrder = -1.0f;
            //construct level based on config file

            String[] levelData = levelConfigFile.text.Split('\n');
            String[] curOrderData;

            //first line is number of orders
            totalLevelOrders = Convert.ToInt32(levelData[0]);

            //rest of lines are the orders
            float tempArrivalTime = 0.0f;
            float tempPrepTime = 0.0f;
            for (int i = 1; i < levelData.Length; i++)
            {
                //data for each order is arranged like so: (arrival time, order, prep time)
                //the input data should be ordered by arrival time
                curOrderData = levelData[i].Split(',');
                tempArrivalTime = Convert.ToSingle(curOrderData[0]);
                Order tempOrderType = orderPrefabs[Convert.ToInt32(curOrderData[1])];
                tempPrepTime = Convert.ToSingle(curOrderData[2]);
                Tuple<float, Order, float> newOrder = new Tuple<float, Order, float>(tempArrivalTime, tempOrderType, tempPrepTime);
                queuedOrders.Enqueue(newOrder);

                //save the first order arrival time as the initial arrival time
                if (i == 1)
                {
                    timeToFirstOrder = Convert.ToSingle(curOrderData[0]);
                }
            }
            //the final end time is the same as the end of the level
            levelEndTime = tempArrivalTime + tempPrepTime;
            return timeToFirstOrder;
        }
    }

    //end level
    void endLevel()
    {
        levelIsEnded = true;

        //remove UI orders that still exist (eg. clock runs out before complete)
        for (int i = 0; i < activeOrders.Count; i++)
        {
            //delete ui
            UIObject.deleteOrderUI(activeOrders[i].Item2);
        }
         
        //TODO camera stuff here

        //TODO pause once camera is complete

        //UI stuff
        //TODO send nOrdersCompleted and completion % to the score UI
        float completionPercent = ((float)nOrdersCompleted) / totalLevelOrders;

        //TODO UI for game win
        if (completionPercent >= 0.8f)
        {
            //TODO
            print("you won!");
        }
        else
        {
            //TODO UI for game loss
            print("you lost!");
        }
    }

    //start
    void Start()
    {
        levelIsEnded = false;
        UIObject = MainUIObject.GetComponent<UIOrderQueueManager>();
        curTime = 0.0f;
        inCountdown = true;
        Time.timeScale = 0.0f;
        score = 0;
        ScoreObject.GetComponent<UIGameScore>().updateGameScore(score);
        timeToNextOrder = GenerateLevelOrders();
    }


    //*****************************************************************************
    //checks to see if we should add new orders from pending queue to active orders
    void Update()
    {
        if (inCountdown)
        {
            if (countdownDuration >= 0.0f)
            {
                countdownDuration -= Time.unscaledDeltaTime;
            }else
            {
                inCountdown = false;
                Time.timeScale = 1.0f;
            }
            return;
        }

        if (!levelIsEnded)
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

            //update the current time. Don't drop below 0
            curTime += Time.deltaTime;
            if (levelEndTime - curTime <= 0.0f) curTime = levelEndTime;

            //update timer UI
            TimeObject.GetComponent<UIGameTimer>().updateGameTimer(levelEndTime - curTime);

            //end game
            if (levelEndTime <= curTime)
            {
                endLevel();
            }
        }
    }
}
