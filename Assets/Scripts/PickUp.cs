﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("PickUp properties")]
    [SerializeField] bool isJumpUpgrade = false;
    [SerializeField] bool isDashUpgrade = false;
    [SerializeField] bool isHealthUpgrade = false;
    [SerializeField] bool isHealing = false;
    [SerializeField] DataLinker dataLinker;
    private Save save;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isJumpUpgrade)
            {
                //dataLinker.saveFile.powerUpDoubleJump = true;
                dataLinker.playerMovement.hasPUDoubleJump = true;
            }

            if (isDashUpgrade)
            {
                //dataLinker.saveFile.powerUpDash = true;
                dataLinker.playerMovement.hasPUDash = true;
            }

            if (isHealthUpgrade)
            {
                // Health Upgrade effect goes here
            }

            if (isHealing)
            {
                // Healing effect goes here
            }

            Destroy(gameObject);
        }
    }
}
