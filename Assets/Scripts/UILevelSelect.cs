using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelect : MonoBehaviour
{

    void Start()
    {
        DisplayHighScore();
    }

    public void GoBack()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("IrisUIMainMenu");
    }

    public void PlayLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NewMainScene");
    }

    public void DisplayHighScore()
    {
        Transform levels = this.gameObject.transform.Find("Levels").transform;

        foreach(Transform level in levels)
        {
            // TO DO: retrieve high score from text file
            // TO DO: get rid of null reference error??
            string highScore = 10.ToString();

            level.gameObject.transform.Find("HighScore").GetComponent<Text>().text = string.Format("High score: {0}", highScore);
        }
    }
        
}
