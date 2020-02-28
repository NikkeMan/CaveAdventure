using UnityEngine;

public class Bat : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] private int health = 3;
    [SerializeField] private float movementSpeed = 1.5f;
    public int attackPower = 1;
    public float attackSpeedMult = 3.0f;

    [Header("AI")]
    [SerializeField] private float AICoolDownTimer = 0.5f;
    [SerializeField] private float AICoolDownTimerMax = 0.5f;

    [Header("Knockback")]
    [SerializeField] private float knockBackForce = 100f;
    [SerializeField] private float knockBackDirX = 1;
    [SerializeField] private float knockBackDirY = 1;

    [Header("Other")]
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] public Animator animator;
    private Rigidbody2D rigidBody;
    private float velocityThreshold = 0.1f;
    private GameObject player;
    private CircleCollider2D attackRange;
    public GameObject itemDrop;

    [SerializeField] bool isVulnerable = true;
    [SerializeField] float invulCooldown = 1f;
    private float invulTimer = 0f;

    private void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        attackRange = gameObject.transform.GetComponentInChildren<CircleCollider2D>();
        player = GameObject.Find("Player");
        animator = gameObject.GetComponent<Animator>();

        rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
    }

    private void Update()
    {
        if (!isVulnerable && invulTimer <= invulCooldown)
        {
            animator.SetBool("TakingDamage", true);
            animator.SetLayerWeight(animator.GetLayerIndex("TakingDamageLayer"), 1);

            invulTimer += Time.deltaTime;
        }

        else
        {
            animator.SetBool("TakingDamage", false);
            animator.SetLayerWeight(animator.GetLayerIndex("TakingDamageLayer"), 0);
        }
    }

    private void FixedUpdate() {
        if (AICoolDownTimer >= AICoolDownTimerMax) {
            Move();
        }
        else if (AICoolDownTimer < AICoolDownTimerMax) {
            AICoolDownTimer += Time.deltaTime;
        }
    }

    private void Move() {
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

    public void TurnAround() {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
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

    public void TakeDamage(int damage) {
        health -= damage;
        AICoolDownTimer = 0;
        isVulnerable = false;
        invulTimer = 0f;

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
        Object.Instantiate(itemDrop, transform.position, Quaternion.identity);
        Object.Destroy(this.gameObject);
    }
}