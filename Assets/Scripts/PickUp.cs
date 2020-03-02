using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Options:")]
    [SerializeField] bool isInstantiated = true;
    [SerializeField] float spawnBump = 150f;

    [SerializeField] bool cooldownOnInstantiate = true;
    [SerializeField] float pickUpCooldown = 1f;
    private float pickUpTimer = 0f;

    [Header("PickUp properties")]
    [SerializeField] bool isJumpUpgrade = false;
    [SerializeField] bool isDashUpgrade = false;
    [SerializeField] bool isHealthUpgrade = false;
    [SerializeField] bool isHealing = false;
    [SerializeField] DataLinker dataLinker;
    private Save save;
    HealthBar healthBar;

    private void Start() {
        healthBar = GameObject.Find("HealthPanel").GetComponent<HealthBar>();
        dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();

        if (isInstantiated)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * spawnBump);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (isInstantiated && cooldownOnInstantiate)
        {
            pickUpTimer += Time.deltaTime;

            if (pickUpTimer >= pickUpCooldown)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isJumpUpgrade)
            {
                //dataLinker.saveFile.powerUpDoubleJump = true;
                dataLinker.playerMovement.hasPUDoubleJump = true;
                dataLinker.popUpScript.ShowDoubleJumpPopUp();
            }

            if (isDashUpgrade)
            {
                //dataLinker.saveFile.powerUpDash = true;
                dataLinker.playerMovement.hasPUDash = true;
                dataLinker.popUpScript.ShowDashPopUp();
            }

            if (isHealthUpgrade)
            {
                healthBar.IncreaseMaxHealth(1);
            }

            if (isHealing && healthBar.healthCurrent < healthBar.healthMax)
            {
                healthBar.Heal(1);
            }

            if (isHealing && healthBar.healthCurrent >= healthBar.healthMax)
            {

            }

            else
            {
                Destroy(gameObject);
            }
        }
    }
}
