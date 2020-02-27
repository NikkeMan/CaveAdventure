using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] Animator transitionAnimator;
    [SerializeField] DataLinker dataLinker;

    [Header("Choose method:")]
    [SerializeField] private bool autoLoadNextScene = true;
    [SerializeField] private bool loadSceneFromSave = false;
    [SerializeField] private bool loadSceneByName = false;
    [SerializeField] private bool loadSceneByIndex = false;

    [Header("If not auto-loading:")]
    [SerializeField] private string sceneName;
    [SerializeField] private int levelToLoad = 0;

    [Header("Wait transition for:")]
    [SerializeField] private float transitionTime = 1f;

    private void Start()
    {
        //dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoRoutine());
    }

    IEnumerator LoadSceneCoRoutine()
    {
        transitionAnimator.SetTrigger("TransitionStart");

        yield return new WaitForSeconds(transitionTime);

        if (autoLoadNextScene && !loadSceneByName && !loadSceneByIndex && !loadSceneFromSave)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else if (loadSceneByName && !autoLoadNextScene && !loadSceneByIndex && !loadSceneFromSave)
        {
            SceneManager.LoadScene(sceneName);
        }

        else if (loadSceneByIndex && !loadSceneByName && !autoLoadNextScene && !loadSceneFromSave)
        {
            SceneManager.LoadScene(levelToLoad);
        }

        else if (loadSceneFromSave && !loadSceneByIndex && !loadSceneByName && !autoLoadNextScene && dataLinker != null)
        {
            SceneManager.LoadScene(dataLinker.saveFile.saveLevel);
        }

        else
        {
            Debug.Log("Too many or no scene load method selected!");
        }
    }
}




