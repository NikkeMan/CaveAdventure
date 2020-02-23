using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour {
    Slime slime;
    [SerializeField] float floorCheckTimerMax = 0.5f;
    [SerializeField] float floorCheckTimerCurrent = 0.5f;

    void Start()
    {
        slime = transform.parent.GetComponent<Slime>();
    }

    private void FixedUpdate() {
        if(floorCheckTimerCurrent < floorCheckTimerMax) {
            // GameObject is in the air:
            floorCheckTimerCurrent += Time.deltaTime;
        }
        if (floorCheckTimerCurrent >= floorCheckTimerMax) {
            // Enable floorCheckBox:
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground") && !slime.isGrounded) {
            // Landed on ground:
            slime.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            // No longer touching ground:

            if (slime.animator.GetBool("isAttacking") == true) {
                // If attacking:
                GetComponent<BoxCollider2D>().enabled = false;
                slime.isGrounded = false;
                floorCheckTimerCurrent = 0;
            }
            else if (slime.animator.GetBool("isAttacking") == false) {
                // If not attacking, cannot move ahead -> turn around:
                slime.TurnAround();
            }
        }

    }
}