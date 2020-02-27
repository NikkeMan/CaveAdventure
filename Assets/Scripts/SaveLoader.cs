using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    [SerializeField] DataLinker dataLinker;
    private void Awake()
    {
        dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();

        dataLinker.checkPointRef.CheckPointLoad();
    }
}
