using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameMenu : MonoBehaviour
{
    GameObject[] pauseObjects;
    GameObject[] endObjects;
    GameObject[] playObjects;

    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        hidePaused();

        endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
       
        playObjects = GameObject.FindGameObjectsWithTag("HideOnEnd");

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

        foreach (GameObject g in playObjects)
        {
            g.SetActive(false);
        }
    }


    public void hideEnd()
    {
        foreach(GameObject g in endObjects)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in playObjects)
        {
            g.SetActive(true);
        }
    }

    public void restartGame()
    {
        Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(activeScene.name);
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
