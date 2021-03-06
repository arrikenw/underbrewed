// Code adapted from Sitepoint: https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameMenu : MonoBehaviour
{
    public bool pauseEnabled;

    public AudioSource gameMusic;

    GameObject[] pauseObjects;
    GameObject[] endObjects;
    GameObject[] playObjects;

    void Start()
    {
        pauseEnabled = false;

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
        if (pauseEnabled)
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

    }

    public void showPaused()
    {
        if (gameMusic)
        {
            gameMusic.Pause();
        }
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        if (gameMusic)
        {
            gameMusic.Play();
        }
        
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showEnd()
    {
        if (gameMusic)
        {
            gameMusic.Stop();
        }
        foreach (GameObject g in endObjects)
        {
            g.SetActive(true);
        }

        foreach (GameObject o in playObjects)
        {
            o.SetActive(false);
        }
    }


    public void hideEnd()
    {
        foreach(GameObject g in endObjects)
        {
            g.SetActive(false);
        }

        foreach (GameObject o in playObjects)
        {
            o.SetActive(true);
        }
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
