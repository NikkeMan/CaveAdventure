using UnityEngine;

public class Slime : Enemy {

    [Header("Slimme stats")]
    [SerializeField] private float jumpForce = 100f;

    private void FixedUpdate() {
        if (AICoolDownTimer >= AICoolDownTimerMax && isGrounded) {
            Move();
        }
        else if (AICoolDownTimer < AICoolDownTimerMax && isGrounded) {
            AICoolDownTimer += Time.deltaTime;
        }
    }

    public void Attack() {
        rigidBody.velocity = Vector2.zero;
        Vector2 jumpDirection = Vector2.zero;
        animator.SetBool("isAttacking", true);
        attackRange.enabled = false;

        if (gameObject.transform.position.x > player.transform.position.x) {
            // Player is on the left side:
            if (isFacingRight) {
                TurnAround();
            }
        }
        else if (gameObject.transform.position.x < player.transform.position.x) {
            // Player is on the right side:
            if (!isFacingRight) {
                TurnAround();
            }
        }
        jumpDirection = CalculateJumpDirection(player.transform.position, transform.position);

        rigidBody.AddForce(jumpDirection * jumpForce);
        AICoolDownTimer = 0;
    }

    // Calculates the jumpDirection based on the direction the player is at:
    private Vector2 CalculateJumpDirection(Vector3 pos1, Vector3 pos2) {
        Vector2 playerDirection = new Vector2(pos1.x - pos2.x, pos1.y - pos2.y).normalized;
        Vector2 jumpDirection = new Vector2(playerDirection.x / 2, (playerDirection.y + 1) / 2);
        return jumpDirection;
    }
}