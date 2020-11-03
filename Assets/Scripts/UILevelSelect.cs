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
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void PlayLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    public void DisplayHighScore()
    {
        Transform levels = this.gameObject.transform.Find("Levels").transform;
        int ct = 0;
        foreach(Transform level in levels)
        {
            string highScore = PlayerPrefs.GetInt("highscore"+ct, 0).ToString();
            level.gameObject.transform.Find("HighScore").GetComponent<Text>().text = string.Format("High score: {0}", highScore);
            ct += 1;
        }
    }
        
}
