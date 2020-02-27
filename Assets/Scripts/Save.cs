using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "CaveAdventure/Save")]
public class Save : ScriptableObject
{
    [Header("Saved Data:")]
    //public Vector2 savePlayerLocation;
    public bool isContinued;
    public int saveHealthCurrent;
    public int saveLevel = 0;
    public int saveCheckpoint = 1;
    public int saveScore = 0;
    public float saveTimer = 0;
    public int saveHealthMax = 0;
    public bool powerUpDoubleJump = false;
    public bool powerUpDash = false;

    [Header("Defaults:")]
    public int saveHealthDefault = 5;
    public int saveLevelDefault = 0;
    public int saveCheckpointDefault = 1;
    public int saveScoreDefault = 0;
    public float saveTimerDefault = 0;
    public int saveHealthMaxDefault = 0;
    public bool powerUpDoubleJumpDefault = false;
    public bool powerUpDashDefault = false;


    public void NewGame()
    {
        // Reset current health, level and checkpoint
        isContinued = false;
        saveHealthCurrent = saveHealthDefault;
        saveLevel = saveLevelDefault;
        saveCheckpoint = saveCheckpointDefault;

        // Reset score and timer
        saveScore = saveScoreDefault;
        saveTimer = saveTimerDefault;

        // Reset health and ability upgrades
        saveHealthMax = saveHealthMaxDefault;
        powerUpDoubleJump = powerUpDoubleJumpDefault;
        powerUpDash = powerUpDashDefault;
    }
}
