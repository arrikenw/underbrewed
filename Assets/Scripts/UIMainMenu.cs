using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{

    public void LoadTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UITutorial");
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("UILevel1");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
