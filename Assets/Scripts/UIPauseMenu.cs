using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{

    public void Resume()
    {
        // TO DO: Resume game
        Debug.Log("TO DO: Resume game");

    }

    public void Restart()
    {
        // TO DO: Resume level
        Debug.Log("TO DO: Restart level");
    }

    public void Quit()
    {
        // TO DO: End game
        UnityEngine.SceneManagement.SceneManager.LoadScene("UIMainMenu");
    }

}
