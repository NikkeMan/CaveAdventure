using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("PickUp properties")]
    [SerializeField] bool isJumpUpgrade = false;
    [SerializeField] bool isDashUpgrade = false;
    [SerializeField] bool isHealing = false;
    [SerializeField] Save save;
    [SerializeField] PlayerMovement pm;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isJumpUpgrade)
            {
                //save.powerUpDoubleJump = true;
                pm.hasPUDoubleJump = true;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (isDashUpgrade)
            {
                //save.powerUpDash = true;
                pm.hasPUDash = true;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Check and Healing effect goes here
        }

        Destroy(gameObject);
    }
}
