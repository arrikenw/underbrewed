using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{

    public void LoadTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisScene");
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
    }


    public void Quit()
    {
        Application.Quit();
    }

}
