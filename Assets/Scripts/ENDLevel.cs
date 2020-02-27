using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDLevel : MonoBehaviour
{
    [SerializeField] SceneSwitch sceneSwitcher;

    private void Start()
    {
        sceneSwitcher = GameObject.Find("SceneLoader").GetComponent<SceneSwitch>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneSwitcher.LoadScene();
        }
    }
}
