using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void ClickQuit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
