using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperHalfController : MonoBehaviour
{
    [Header("Flight Settings")]
    public float ascendSpeed = 5f; // ascend velocity when player press jump
    public float descendSpeed = 1f;  // slow fall speed after hang time
    public float hangTime = 1f;    // time character can stay airborne after a jump

    [Header("Ground Hover Settings")]
    public float hoverBobAmount = 0.1f; // how much the hover bobbing moves
    public float hoverBobSpeed = 5f;    // how fast the bobbing moves

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;
    private float hangTimeCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // disables default gravity
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            hangTimeCounter = hangTime;
        }

        // cancel jump early
        if(Input.GetButtonUp("Jump") && isJumping)
        {
            hangTimeCounter = 0f;
        }
    }

    void FixedUpdate()
    {

        if(isJumping)
        {
           if(hangTimeCounter > 0)
            {
                //rise up
                rb.velocity = new Vector2(rb.velocity.x, ascendSpeed);
                hangTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                //start to fall slowly
                rb.velocity = new Vector2(rb.velocity.x, descendSpeed);

                //exit jumping state
                if (!isGrounded)
                {
                    isJumping = false;
                }
            }
        }
        else
        {
            if(!isGrounded)
            {
                //fall slowly
                rb.velocity = new Vector2(rb.velocity.x, -descendSpeed);
            }
            else
            {
                //apply hover bobbing when near the ground
                float hoverY = Mathf.Sin(Time.time * hoverBobSpeed) * hoverBobAmount;
                rb.velocity = new Vector2(rb.velocity.x, hoverY);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}
