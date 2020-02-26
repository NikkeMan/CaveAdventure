using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ClickPause();
        }
    }

    public void ClickPause()
    {
        if (isPaused)
        {
            isPaused = false;
            PauseGame();
        }

        else
        {
            isPaused = true;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
