using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    enum TutorialState
    {
        Welcome,
        LearnMovement,
        LearnPickUp,
        LearnThrowing,
        LearnStation,
        UseCrush,
        UseBurn,
        UseChop,
        LearnOrder,
        MakeGood,
        UsePotionGood,
        UsePortal,
        MakeDud,
        UsePotionDud,
        UseBin,
        GoodLuck        
    }


    private TutorialState curState = TutorialState.Welcome;
    public AudioSource soundEffect;
    public GameObject UIObject;
    public GameObject MRCAULDRON;

    void Update()
    {
        if (UIObject.transform.childCount == 0)
        {
            UIObject.SetActive(false);
            MRCAULDRON.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curState == TutorialState.Welcome) {
                OnWelcome();
                return;
            }
            if (curState == TutorialState.LearnMovement)
            {
                OnLearnMovement();
                return;
            }
            if (curState == TutorialState.LearnStation)
            {
                OnLearnStation();
                return;
            }
            if (curState == TutorialState.LearnThrowing)
            {
                OnLearnThrowing();
                return;
            }
            if (curState == TutorialState.LearnOrder)
            {
                OnLearnOrder();
                return;
            }
        }
    }

    //nextMessage
    private void nextMessage()
    {
        if (UIObject.activeSelf)
        {
            if (UIObject.transform.childCount >= 2)
            {
                UIObject.transform.GetChild(1).gameObject.SetActive(true);
                Destroy(UIObject.transform.GetChild(0).gameObject);
            }
        }
        soundEffect.Play();
    }


    //*************************************************
    //intro
    public void OnWelcome()  //space to progress
    {
        if (curState == TutorialState.Welcome)
        {
            curState = TutorialState.LearnMovement;
            nextMessage();
            print("moving to learnmovement");
        }
    }

    //*************************************************
    //teaching controls
    public void OnLearnMovement()  //space to progress
    {
        if (curState == TutorialState.LearnMovement)
        {
            curState = TutorialState.LearnPickUp;
            nextMessage();
            print("moving to pickup");
        }
    }

    public void OnLearnPickup()  //implemented
    {
        if (curState == TutorialState.LearnPickUp)
        {
            curState = TutorialState.LearnThrowing;
            nextMessage();
            print("moving to station");
        }
    }

    public void OnLearnThrowing()  //implemented
    {
        if (curState == TutorialState.LearnThrowing)
        {
            curState = TutorialState.LearnStation;
            nextMessage();
            print("moving to station");
        }
    }

    //*************************************************
    //teaching stations
    public void OnLearnStation()  //space to progress
    {
        if (curState == TutorialState.LearnStation)
        {
            curState = TutorialState.UseCrush;
            nextMessage();
            print("moving to crush");
        }
    }

    public void OnUseCrush() //implemented
    {
        if (curState == TutorialState.UseCrush)
        {
            curState = TutorialState.UseBurn;
            nextMessage();
            print("moving to burn");
        }
    }

    public void OnUseBurn() //implemented
    {
        if (curState == TutorialState.UseBurn)
        {
            curState = TutorialState.UseChop;
            nextMessage();
            print("moving to chop");
        }
    }

    public void OnUseChop() //implemented
    {
        if (curState == TutorialState.UseChop)
        {
            curState = TutorialState.LearnOrder;
            nextMessage();
            print("moving to learn order");
        }
    }

    public void OnLearnOrder() //press space
    {
        if (curState == TutorialState.LearnOrder)
        {
            curState = TutorialState.MakeGood;
            nextMessage();
            print("moving to make potion");
        }
    }

    //*************************************************
    //dealing with good recipe
    public void OnGoodRecipeCreated()  //implemented
    {
        if (curState == TutorialState.MakeGood)
        {
            curState = TutorialState.UsePotionGood;
            nextMessage();
            print("moving to fill potion");
        }
    }

    public void OnUsePotionGood() //implemented but kinda crap X D
    {
        if (curState == TutorialState.UsePotionGood)
        {
            curState = TutorialState.UsePortal;
            nextMessage();
            print("moving to use portal");
        }
    }

    public void OnUsePortal() //implemented
    {
        if (curState == TutorialState.UsePortal)
        {
            curState = TutorialState.MakeDud;
            nextMessage();
            print("moving to make bad recipe");
        }
    }

    //*************************************************
    //dealing with bad recipes
    public void OnBadRecipeCreated() //implemented
    {
        if (curState == TutorialState.MakeDud)
        {
            curState = TutorialState.UsePotionDud;
            nextMessage();
            print("moving to fill potion bad");
        }
    }

    public void OnUsePotionDud() //implmented
    {
        if (curState == TutorialState.UsePotionDud)
        {
            curState = TutorialState.UseBin;
            nextMessage();
            print("moving to use bin");
        }
    }

    public void OnUseBin() //implemented
    {
        if (curState == TutorialState.UseBin)
        {
            curState = TutorialState.GoodLuck;
            nextMessage();
            print("moving to last msg");
        }
    }
}
