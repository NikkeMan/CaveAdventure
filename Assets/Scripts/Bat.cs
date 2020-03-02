using UnityEngine;

public class Bat : Enemy {

    [Header("Bat stats")]
    public float attackSpeedMult = 3.0f;

    private void FixedUpdate() {
        if (AICoolDownTimer >= AICoolDownTimerMax) {
            Move();
        }
        else if (AICoolDownTimer < AICoolDownTimerMax) {
            AICoolDownTimer += Time.deltaTime;
        }
    }

    public override void Move() {
        // Sometimes the enemy gets stuck when facing a wall and velocity is not 0, that's why we use a threshold (for example -0.1 < velocity < +0.1)
        if (rigidBody.velocity.x >= -velocityThreshold && rigidBody.velocity.x <= velocityThreshold) {
            // Is against a wall, turn around:
            TurnAround();
        }

        if (animator.GetBool("isAttacking")) {
            //calculate attack direction:
            Vector2 playerDirection = CalculateAttackDirection(player.transform.position, transform.position);
            rigidBody.velocity = new Vector2(playerDirection.x * attackSpeedMult, playerDirection.y * attackSpeedMult);
        }

        if (!animator.GetBool("isAttacking")) {
            if (isFacingRight) {
                rigidBody.velocity = new Vector2(movementSpeed, 0);
            }
            else {
                rigidBody.velocity = new Vector2(-movementSpeed, 0);
            }
        }
    }

    public void Attack() {
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
    }

    // Calculates the normalized playerDirection:
    private Vector2 CalculateAttackDirection(Vector3 pos1, Vector3 pos2) {
        Vector2 playerDirection = new Vector2(pos1.x - pos2.x, pos1.y - pos2.y).normalized;
        return playerDirection;
    }
}