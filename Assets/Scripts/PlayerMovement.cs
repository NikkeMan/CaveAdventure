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

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    public Vector2 direction;
    bool facingRight = true;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpBuffer;
    float jumpBufferTimer;

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

    // [Header("Animation")]

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        onGround = Physics2D.Raycast(groundCheck.transform.position + colliderOffset, Vector2.down, groundCheckLength, groundLayer) || 
            Physics2D.Raycast(groundCheck.transform.position - colliderOffset, Vector2.down, groundCheckLength, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = Time.time + jumpBuffer;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        MovePlayer(direction.x);
        ModifyPhysics();

        if (jumpBufferTimer > Time.time && onGround)
        {
            Jump();
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
