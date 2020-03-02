using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Stats")]
    [SerializeField] protected int health = 3;
    [SerializeField] protected float movementSpeed = 1.5f;
    public int attackPower = 1;

    [Header("AI")]
    [SerializeField] protected float AICoolDownTimer = 0.5f;
    [SerializeField] protected float AICoolDownTimerMax = 0.5f;

    [Header("Knockback")]
    [SerializeField] protected float knockBackForce = 100f;
    [SerializeField] protected float knockBackDirX = 1;
    [SerializeField] protected float knockBackDirY = 1;

    [Header("Other")]
    [SerializeField] protected bool isFacingRight = true;

    [SerializeField] public bool isGrounded = true;
    [SerializeField] public Animator animator;
    protected Rigidbody2D rigidBody;
    [SerializeField] protected float velocityThreshold = 0.1f;
    protected GameObject player;
    protected CircleCollider2D attackRange;
    public GameObject itemDrop;

    [SerializeField] protected bool isVulnerable = true;
    [SerializeField] protected float invulCooldown = 1f;
    [SerializeField] protected float invulTimer = 0f;

    protected void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        attackRange = gameObject.transform.GetComponentInChildren<CircleCollider2D>();
        player = GameObject.Find("Player");
        animator = gameObject.GetComponent<Animator>();

        rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);
    }

    protected void Update() {
        if (!isVulnerable && invulTimer <= invulCooldown) {
            animator.SetBool("TakingDamage", true);
            animator.SetLayerWeight(animator.GetLayerIndex("TakingDamageLayer"), 1);

            invulTimer += Time.deltaTime;
        }
        else {
            isVulnerable = true;
            animator.SetBool("TakingDamage", false);
            animator.SetLayerWeight(animator.GetLayerIndex("TakingDamageLayer"), 0);
        }
    }

    public virtual void Move() {
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

    public void TakeDamage(int damage) {
        if (isVulnerable) {
            health -= damage;
            if (health <= 0) {
                Die();
            }
            else {
                KnockBack();
                AICoolDownTimer = 0;
                isVulnerable = false;
                invulTimer = 0f;
            }
        }
    }

    public void KnockBack() {
        if (isVulnerable) {
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
    }

    protected void Die() {
        Object.Instantiate(itemDrop, transform.position, Quaternion.identity);
        Object.Destroy(this.gameObject);
    }
}