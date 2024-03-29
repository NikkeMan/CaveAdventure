﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] DataLinker dataLinker;

    private void Start()
    {
        dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();
    }

    public void NewGameClick()
    {
        dataLinker.saveFile.NewGame();
    }
}
