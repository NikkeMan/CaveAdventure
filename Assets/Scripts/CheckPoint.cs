using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private DataLinker dataLinker;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Score score;
    //[SerializeField] private SceneSwitch sceneSwitch;
    // [SerializeField] private Transform checkPointTransform;

    private void Start()
    {
        dataLinker.saveFile.saveLevel = SceneManager.GetActiveScene().buildIndex;
        //dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();
        //healthBar = GameObject.Find("HealthPanel").GetComponent<HealthBar>();
        //score = GameObject.Find("ScorePanel").GetComponent<Score>();
        //sceneSwitch = GameObject.Find("SceneLoader").GetComponent<SceneSwitch>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dataLinker.saveFile.saveLevel = SceneManager.GetActiveScene().buildIndex + 1;
            CheckPointSave();
        }
    }

    public void CheckPointSave()
    {
        dataLinker.saveFile.isContinued = true;
        dataLinker.saveFile.saveHealthCurrent = healthBar.healthCurrent;
        //dataLinker.saveFile.saveLevel = SceneManager.GetActiveScene().buildIndex;
        dataLinker.saveFile.saveScore = score.currentScore;
        dataLinker.saveFile.saveHealthMax = healthBar.healthMax;
        dataLinker.saveFile.powerUpDoubleJump = dataLinker.playerMovement.hasPUDoubleJump;
        dataLinker.saveFile.powerUpDash = dataLinker.playerMovement.hasPUDash;
    }

    public void CheckPointLoad()
    {
        healthBar.healthMax = dataLinker.saveFile.saveHealthMax;
        healthBar.healthCurrent = dataLinker.saveFile.saveHealthCurrent;
        score.currentScore = dataLinker.saveFile.saveScore;
        // Timer
        dataLinker.playerMovement.hasPUDoubleJump = dataLinker.saveFile.powerUpDoubleJump;
        dataLinker.playerMovement.hasPUDash = dataLinker.saveFile.powerUpDash;
    }
}
