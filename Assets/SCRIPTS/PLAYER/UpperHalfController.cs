using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperHalfController : MonoBehaviour
{
    [Header("Jump Settings")]
    public float initialJumpForce = 10f;        // First jump force
    public float doubleJumpForce = 5f;         // Second jump force (half of initial)
    public float maxJumpHeight = 3f;           // Maximum height before falling
    public float fallSpeed = 8f;              // Downward speed after peak
    public float jumpCooldown = 0.5f;         // Time before next jump

    [Header("Ground Hover Settings")]
    public float hoverBobAmount = 0.1f;
    public float hoverBobSpeed = 5f;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Control Toggle")]
    public bool canControl = true;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float hoverBaseY;
    private bool wasGroundedLastFrame;
    private float lastHoverY;
    private float lastJumpTime;
    private bool isJumping;
    private float jumpStartY;
    private bool canDoubleJump;  // Track if double jump is available

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        hoverBaseY = transform.position.y;
        lastHoverY = 0f;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset double jump when grounded
        if (isGrounded && !wasGroundedLastFrame)
        {
            hoverBaseY = transform.position.y - lastHoverY;
            lastHoverY = 0f;
            isJumping = false;
            canDoubleJump = true;  // Reset double jump when landing
        }

        wasGroundedLastFrame = isGrounded;

        if (!canControl)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, 0);
            }
            return;
        }

        // Horizontal movement
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Jump logic
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && Time.time > lastJumpTime + jumpCooldown)
            {
                // Initial jump
                rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);
                lastJumpTime = Time.time;
                isJumping = true;
                jumpStartY = transform.position.y;
            }
            else if (canDoubleJump && !isGrounded)
            {
                // Double jump (half power)
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                canDoubleJump = false;  // Used up double jump
                isJumping = true;
                jumpStartY = transform.position.y;  // Reset jump height measurement
            }
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            // Check if we've reached max height
            if (transform.position.y >= jumpStartY + maxJumpHeight)
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
            }
        }
        else if (!isGrounded)
        {
            // Apply constant downward force when not grounded and not jumping
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        }

        // Hover logic
        if (isGrounded && !isJumping)
        {
            float hoverY = Mathf.Sin(Time.time * hoverBobSpeed) * hoverBobAmount;

            if (canControl)
            {
                rb.velocity = new Vector2(rb.velocity.x, (hoverBaseY + hoverY - transform.position.y) * hoverBobSpeed);
            }
            else
            {
                rb.velocity = new Vector2(0, (hoverBaseY - transform.position.y) * hoverBobSpeed);
            }

            lastHoverY = hoverY;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}