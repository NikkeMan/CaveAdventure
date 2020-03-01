using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDLevel : MonoBehaviour
{
    [SerializeField] SceneSwitch sceneSwitcher;
    [SerializeField] DataLinker dataLinker;

    private void Start()
    {
        sceneSwitcher = GameObject.Find("SceneLoader").GetComponent<SceneSwitch>();
        dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            //++dataLinker.saveFile.saveLevel;
            sceneSwitcher.LoadScene();
        }
    }
}
