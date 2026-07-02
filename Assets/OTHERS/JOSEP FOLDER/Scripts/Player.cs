using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("PLAYER TYPE")]
    public PlayerType playerType;

    [Header("MOVEMENT")]
    public bool canControl = true;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("CUSTOM KEYS")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("JUMP COOLDOWN")]
    public float jumpCooldown = 0.5f; // seconds between jumps
    private float nextJumpTime = 0f;

    [Header("GROUNDCHECK")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    //ANIMATION
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isFacingRight = true;

    //AUDIO
    private bool playingFootsteps = false;
    public float footstepSpeed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canControl)
        {
            stopFootsteps();
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Custom Movement ---
        float move = 0f;
        if (Input.GetKey(leftKey))
            move = -1f;
        else if (Input.GetKey(rightKey))
            move = 1f;

        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Animator updates
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isJumping", !isGrounded);

        // Flip Sprite
        FlipSprite(move);

        // Jump with cooldown
        if (Input.GetKeyDown(jumpKey) && isGrounded && Time.time >= nextJumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            nextJumpTime = Time.time + jumpCooldown;
        }

        // Start Footsteps
        if (Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded && !playingFootsteps)
        {
            startFootsteps();
        }
        else if (Mathf.Abs(rb.velocity.x) <= 0.1f || !isGrounded)
        {
            stopFootsteps();
        }
    }

    void startFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(playFootsteps), 0f, footstepSpeed);
    }

    void stopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(playFootsteps));
    }

    void playFootsteps()
    {
        Debug.Log("Playing Footstep Sound");
        SoundEffectManager.Play("Footsteps");
    }

    private void FlipSprite(float moveInput)
    {
        if ((isFacingRight && moveInput < 0f) || (!isFacingRight && moveInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
}
