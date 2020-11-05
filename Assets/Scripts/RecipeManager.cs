using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipeManager : MonoBehaviour
{

    //tutorial config
    public bool isTutorial;
    public GameObject tutorialManager;

    //level number
    //VERY IMPORTANT FOR STORING HIGHSCORES
    public int levelNumber;

    //Camera
    public GameObject Camera;

    //UI object
    public GameObject MainUIObject;
    protected UIOrderQueueManager UIObject;

    //End screen object
    public GameObject EndLevelObject;

    // Game text 
    public GameObject TextObject;

    // Game menu
    public GameObject MenuObject;

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


                //if in tutorial
                if (isTutorial)
                {
                    tutorialManager.GetComponent<TutorialScript>().OnUsePortal();
                }


                //run endgame logic if the last request was completed and there are no more queued orders
                if (activeOrders.Count == 0 && queuedOrders.Count == 0)
                {
                    //don't end level if we are in the tutorial
                    if (!isTutorial)
                    {
                        endLevel();
                    }
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
        //always just use the first prefab for the example
        Order exampleOrder = orderPrefabs[0];

        //create and store a new UI object
        float timeUntilOrderExpiry = 600000.0f;
        GameObject newActiveOrderUI = UIObject.addOrderUI(exampleOrder, timeUntilOrderExpiry);

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
            levelEndTime = 0.0f;
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

                if (tempArrivalTime + tempPrepTime > levelEndTime)
                {
                    levelEndTime = tempArrivalTime + tempPrepTime;
                }

                //save the first order arrival time as the initial arrival time
                if (i == 1)
                {
                    timeToFirstOrder = Convert.ToSingle(curOrderData[0]);
                }
            }
            //the final end time is the same as the end of the level
            return timeToFirstOrder;
        }
    }

    //end level
    void endLevel()
    {
        

        levelIsEnded = true;
        Time.timeScale = 0.0f;
        //remove UI orders that still exist (eg. clock runs out before complete)
        for (int i = 0; i < activeOrders.Count; i++)
        {
            //delete ui
            UIObject.deleteOrderUI(activeOrders[i].Item2);
        }

        //camera stuff here
        Camera.GetComponent<animateCamera>().EndAnimation();

        // disable pause menu
        MenuObject.GetComponent<UIGameMenu>().pauseEnabled = false;

        // Store and display stats / score
        //calculate completion %
        float completionPercent = 100*(((float)nOrdersCompleted) / totalLevelOrders);

        //store highscore if greater than current highscore
        StoreHighscore(score);
        
        //display stats in UI
        EndLevelObject.GetComponent<UIEndScreen>().updateGameStatistics(completionPercent, score, PlayerPrefs.GetInt("highscore"+levelNumber, 0));

        //end game stats / retry stats
        if (completionPercent >= 0.8f)
        {
            EndLevelObject.GetComponent<UIEndScreen>().updateTitleText(true);
            
        }
        else
        {
            EndLevelObject.GetComponent<UIEndScreen>().updateTitleText(false);
        }

        // Display end screen
        MenuObject.GetComponent<UIGameMenu>().showEnd();
    }

    //store highscore code has been retrieved from:
    //https://answers.unity.com/questions/644911/how-do-i-store-highscore-locally-c-simple.html
    void StoreHighscore(int newHighscore)
    {
        int oldHighscore = PlayerPrefs.GetInt("highscore"+levelNumber, 0);
        if (newHighscore > oldHighscore)
            PlayerPrefs.SetInt("highscore"+levelNumber, newHighscore);
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
        MenuObject.GetComponent<UIGameMenu>().pauseEnabled = false;
        ScoreObject.GetComponent<UIGameScore>().updateGameScore(score);
        timeToNextOrder = GenerateLevelOrders();

        if (!isTutorial)
        {
            StartCoroutine(TextObject.GetComponent<UIGameText>().startText(1.2f));
        }

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
                MenuObject.GetComponent<UIGameMenu>().pauseEnabled = true;
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
