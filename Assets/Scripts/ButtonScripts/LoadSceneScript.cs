using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    [SerializeField] int sceneToLoad = 0;
    public void SceneLoader()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}
