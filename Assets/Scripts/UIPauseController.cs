using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseController : MonoBehaviour
{
    public Transform pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pauseMenu.gameObject.activeInHierarchy == false)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0; // stop time-dependent functions
        }
    }
}
