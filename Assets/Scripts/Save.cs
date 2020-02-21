using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "CaveAdventure/Save")]
public class Save : ScriptableObject
{
    [Header("PlayerInfo")]
    public Vector2 savePlayerLocation;
    public int saveHealth;

    [Header("Defaults")]
    public int maxHealth = 100;

    [Header("Progression")]
    public int saveLevel;
    public int saveScore;
    public bool powerUpDoubleJump;
    public bool powerUpDash;


    void NewGame()
    {
        saveLevel = 0;

        // Reset health and health upgrades here

        //currentPlayerLocation = startingLocation;

        powerUpDoubleJump = false;
        powerUpDash = false;
    }

    void Respawn()
    {
        saveHealth = maxHealth;
    }
}
