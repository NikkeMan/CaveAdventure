using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "CaveAdventure/Save")]
public class Save : ScriptableObject
{
    [Header("PlayerInfo")]
    public Vector2 savePlayerLocation;
    public int saveHealth;

    [Header("Progression")]
    public int saveLevel;
    public int saveScore;
    public bool powerUpDoubleJump;
    public bool powerUpDash;


    void NewGame()
    {
        saveLevel = 0;
        //currentPlayerLocation = startingLocation;

        powerUpDoubleJump = false;
        powerUpDash = false;
    }
}
