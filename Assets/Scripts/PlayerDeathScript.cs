using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    [SerializeField] private DataLinker dataLinker;
    [SerializeField] private SceneSwitch sceneSwitch;
    [SerializeField] private HealthBar healthBar;


    private void Update()
    {
        if (healthBar.playerDead)
        {
            healthBar.playerDead = false;
            sceneSwitch.LoadScene();
        }
    }
}
