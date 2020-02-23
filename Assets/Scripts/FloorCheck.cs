using UnityEngine;

public class FloorCheck : MonoBehaviour {
    [SerializeField] private float floorCheckTimer = 0.5f;
    [SerializeField] private float floorCheckTimerMax = 0.5f;
    private Slime slime;

    private void Start() {
        slime = transform.parent.GetComponent<Slime>();
    }

    private void FixedUpdate() {
        if (floorCheckTimer < floorCheckTimerMax) {
            // GameObject is in the air:
            floorCheckTimer += Time.deltaTime;
        }
        if (floorCheckTimer >= floorCheckTimerMax) {
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
                floorCheckTimer = 0;
            }
            else if (slime.animator.GetBool("isAttacking") == false) {
                // If not attacking, cannot move ahead -> turn around:
                slime.TurnAround();
            }
        }
    }
}