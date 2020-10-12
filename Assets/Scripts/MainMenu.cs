using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void TutorialButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("testTutorial");
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("testLevel1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
