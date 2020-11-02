using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    GameObject[] pauseObjects;

    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            PauseGame();
        }
    }

    public void PauseGame()
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
    public void RestartGame()
    {
        //// TO DO: Restart game
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisScene");
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisUIMainMenu");
    }

}
