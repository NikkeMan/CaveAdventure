using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    [SerializeField] int sceneToLoad = 0;
    [SerializeField] bool loadByNameInstead = false;
    [SerializeField] string sceneName;
    public void SceneLoader()
    {
        Time.timeScale = 1;

        if (!loadByNameInstead)
            SceneManager.LoadScene(sceneToLoad);

        else
            SceneManager.LoadScene(sceneName);
    }
}
