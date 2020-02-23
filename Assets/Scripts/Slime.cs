using UnityEngine;

public class Slime : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] private int health = 3;
    [SerializeField] private float movementSpeed = 1.5f;
    [SerializeField] private float jumpForce = 100f;
    public int attackPower = 1;

    [Header("AI")]
    [SerializeField] private float AICoolDownTimer = 0.5f;
    [SerializeField] private float AICoolDownTimerMax = 0.5f;

    [Header("Knockback")]
    [SerializeField] private float knockBackForce = 100f;
    [SerializeField] private float knockBackDirX = 1;
    [SerializeField] private float knockBackDirY = 1;

    [Header("Other")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] public bool isGrounded = true;
    [SerializeField] public Animator animator;
    private Rigidbody2D rigidBody;
    private float velocityThreshold = 0.1f;
    private GameObject player;
    private CircleCollider2D attackRange;

    // Start is called before the first frame update
    private void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        attackRange = gameObject.transform.GetComponentInChildren<CircleCollider2D>();
        player = GameObject.Find("Player");
        animator = gameObject.GetComponent<Animator>();

        rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
    }

    private void FixedUpdate() {
        if (AICoolDownTimer >= AICoolDownTimerMax && isGrounded) {
            Move();
        }
        else if (AICoolDownTimer < AICoolDownTimerMax && isGrounded) {
            AICoolDownTimer += Time.deltaTime;
        }
    }

    private void Move() {
        animator.SetBool("isAttacking", false);
        attackRange.enabled = true;

        // Sometimes the enemy gets stuck when facing a wall and velocity is not 0, that's why we use a threshold (for example -0.1 < velocity < +0.1)
        if (rigidBody.velocity.x >= -velocityThreshold && rigidBody.velocity.x <= velocityThreshold) {
            // Is against a wall, turn around:
            TurnAround();
        }

        if (isFacingRight) {
            rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
        }
        else {
            rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);
        }
    }

    public void TurnAround() {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
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

    public void TakeDamage(int damage) {
        health -= damage;
        AICoolDownTimer = 0;

        if (health <= 0) {
            Die();
        }
        else {
            KnockBack();
        }
    }

    public void KnockBack() {
        rigidBody.velocity = Vector2.zero;
        Vector2 knockBackDir = Vector2.zero;

        if (gameObject.transform.position.x > player.transform.position.x) {
            // Player is on the left side:
            knockBackDir = new Vector2(knockBackDirX, knockBackDirY);
        }
        else if (gameObject.transform.position.x < player.transform.position.x) {
            // Player is on the right side:
            knockBackDir = new Vector2(-knockBackDirX, knockBackDirY);
        }

        rigidBody.AddForce(knockBackDir * knockBackForce);
        AICoolDownTimer = 0;
    }

    private void Die() {
        Object.Destroy(this.gameObject);
    }
}