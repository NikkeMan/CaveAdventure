using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] Save save;
    [SerializeField] int sceneToLoad;
    
    void Start()
    {
        if (save != null)
        {
            sceneToLoad = save.saveLevel;
        }
        else
        {
            sceneToLoad = 0;
        }

        if (!save.isContinued)
        {
            gameObject.SetActive(false);
        }
    }

    public void ClickContinue()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
