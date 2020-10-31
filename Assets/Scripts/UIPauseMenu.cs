using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public Transform canvas;

    public void ResumeGame()
    {
        // TO DO: Resume game
        if (canvas.gameObject.activeInHierarchy == true)
        {
            Time.timeScale = 1; // for time-dependent functions
            canvas.gameObject.SetActive(false);
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
