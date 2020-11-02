using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameMenu : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] endObjects; 

    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();

        endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
        hideEnd();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            pauseGame();
        }
    }

    public void pauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showEnd()
    {
        foreach (GameObject g in endObjects)
        {
            g.SetActive(true);
        }
    }


    public void hideEnd()
    {
        foreach(GameObject g in endObjects)
        {
            g.SetActive(false);
        }
    }

    public void restartGame()
    {
        //// TO DO: Restart game
        UnityEngine.SceneManagement.SceneManager.LoadScene("NewMainScene");
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisUIMainMenu");
    }

}
