using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSelect : MonoBehaviour
{

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisUIMainMenu");
    }


    public void PlayLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NewMainScene");
    }
        
}
