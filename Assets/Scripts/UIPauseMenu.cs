using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        // TO DO: Resume game
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisScene");
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
