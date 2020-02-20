using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] GameObject groundCheck;
    [SerializeField] Save save;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    public Vector2 direction;
    Vector2 directionMemory;
    bool facingRight = true;
    public bool playerInputsDisabled = false;

    [Header("Attack")]
    [SerializeField] GameObject attackBox;
    [SerializeField] float attackCooldown = 1f;
    float attackCooldownTimer = 0f;
    bool isAttackButtonDown = false;

    [Header("Dash")]
    [SerializeField] float dashDistance = 3f;
    [SerializeField] float dashCooldown = 1f;
    float dashCooldownTimer = 1f;
    [SerializeField] bool dashBump = false;
    [SerializeField] float dashBumpAmount = 0f;
    //bool hasDashed = false;
    bool isDashButtonDown = false;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpBuffer;
    float jumpBufferTimer;
    [SerializeField] bool canDoubleJump = false;

    [Header("Physics")]
    [SerializeField] float maxSpeed = 8f;
    [SerializeField] float linearDrag = 4f;
    [SerializeField] float gravity = 1f;
    [SerializeField] float fallMult = 5f;
    [SerializeField] float airDragMult = 0.15f;

    [Header("Collision")]
    public bool onGround = false;
    [SerializeField] float groundCheckLength = 0.6f;
    [SerializeField] Vector3 colliderOffset;

    [Header("PowerUps")]
    [SerializeField] bool hasPUDoubleJump = false;
    [SerializeField] bool hasPUDash = false;

    // [Header("Animation")]

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackBox.SetActive(false);

        hasPUDoubleJump = save.powerUpDoubleJump;
        hasPUDash = save.powerUpDash;
    }

    
    void Update()
    {
        onGround = Physics2D.Raycast(groundCheck.transform.position + colliderOffset, Vector2.down, groundCheckLength, groundLayer) || 
            Physics2D.Raycast(groundCheck.transform.position - colliderOffset, Vector2.down, groundCheckLength, groundLayer);

        if (!playerInputsDisabled)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferTimer = Time.time + jumpBuffer;
            }

            if (Input.GetButtonDown("Fire3") && dashCooldownTimer <= 0 && hasPUDash)
            {
                isDashButtonDown = true;
            }

            if (Input.GetButtonDown("Fire1") && attackCooldownTimer <= 0)
            {
                isAttackButtonDown = true;
            }

            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        else
        {
            direction = new Vector2(0, 0);
            rb.velocity = new Vector2(0, 0);
        }

        if (direction.x != 0 || direction.y != 0)
        {
            directionMemory = direction;
        }

        if (onGround)
        {
            canDoubleJump = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
        }

        else 
        {
            animator.SetBool("isGrounded", false);
        }


        Attack();
    }

    void FixedUpdate()
    {
        MovePlayer(direction.x);
        ModifyPhysics();
        Dash();

        if (jumpBufferTimer > Time.time)
        {
            if (onGround)
            {
                Jump();
                animator.SetBool("isJumping", true);
            }
            else
            {
                if (canDoubleJump && hasPUDoubleJump)
                {
                    Jump();
                    canDoubleJump = false;
                    animator.SetBool("isDoubleJumping", true);
                }
            }
        }
    }

    void Attack()
    {
        if (attackCooldownTimer >= 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (isAttackButtonDown)
        {
            if (attackCooldownTimer <= 0)
            {
                animator.SetTrigger("Attack");
                isAttackButtonDown = false;
                attackCooldownTimer = attackCooldown;

                // Actual attacking here
                //attackBox.SetActive(true);
            }
        }
    }

    void Dash()
    {
        if (dashCooldownTimer >= 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashButtonDown)
        {
            if (dashCooldownTimer <= 0)
            {
                //rb.velocity = new Vector2(0, 0);
                rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + new Vector2(directionMemory.x, 0) * dashDistance);

                if (dashBump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, dashBumpAmount);
                }

                isDashButtonDown = false;
                dashCooldownTimer = dashCooldown;
            }
        }
    }

    void MovePlayer(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpBufferTimer = 0;
    }

    void ModifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0f;
        }

        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * airDragMult;

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMult;
            }

            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMult / 2);
            }

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        //transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.transform.position + colliderOffset, groundCheck.transform.position + colliderOffset + Vector3.down * groundCheckLength);
        Gizmos.DrawLine(groundCheck.transform.position - colliderOffset, groundCheck.transform.position - colliderOffset + Vector3.down * groundCheckLength);
    }
}
