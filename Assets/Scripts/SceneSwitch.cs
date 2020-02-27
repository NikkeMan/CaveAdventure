using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] Animator transitionAnimator;

    [Header("Choose method:")]
    [SerializeField] private bool autoLoadNextScene = true;
    [SerializeField] private bool loadSceneByName = false;

    [Header("If not auto-loading:")]
    [SerializeField] private string sceneName;
    [SerializeField] private int levelToLoad = 0;

    [Header("Wait transition for:")]
    [SerializeField] private float transitionTime = 1f;

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoRoutine());
    }

    IEnumerator LoadSceneCoRoutine()
    {
        transitionAnimator.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);

        if (autoLoadNextScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else if (loadSceneByName && !autoLoadNextScene)
        {
            SceneManager.LoadScene(sceneName);
        }

        else if (!loadSceneByName && !autoLoadNextScene)
        {
            SceneManager.LoadScene(levelToLoad);
        }

        else
        {
            Debug.Log("No Scene Load method selected!");
        }
    }
}




