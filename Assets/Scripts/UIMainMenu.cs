using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{

    public void LoadTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1_Iris_ver");
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect_Iris_ver");
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
