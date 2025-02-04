using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D charac1RB; // Placeholder for your character object
    public Animator animator; // Placeholder for the character's animations
    public Transform groundCheck; // Collider Checker
    public LayerMask groundLayer; // To specify what object touched/collide an object

    public float speed; // For how fast the player could move
    public float jumpingPower; // For how high the player could jump

    public float horizontal; // Character's placeholder for left and right displacement 
    public float vertical; // Character's placeholder for up and down displacement 

    public bool isWalking; // Character's placeholder to determine if animation should be executed to match the actions
    private bool isFacingRight = true; // For flipping the character based on which direction it's moving

    void Start()
    {
        charac1RB = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterMovement();
        //HandleAnimation();
        Flip();
    }

    void CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get's the key inputs from the Input Manager (Alternative method for "GetKey" functions)

        if (Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            charac1RB.velocity = new Vector2(charac1RB.velocity.x, jumpingPower); // JUMP
        }

        isWalking = horizontal != 0 ? true : false; // Shortest If-Else Statement (Shortcut Method)
    }

    //void HandleAnimation()
    //{
    //    animator.SetBool("isWalking", isWalking); // Animation's on/off switch in order for the computer to know when to trigger the animation and when to stop
    //}

    void Flip()
    {               // Flip to Left                    // Flip to Right
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            // Responsible for switching the value true or false (On and Off)
            isFacingRight = !isFacingRight;

            // Responsible for Flipping/Mirroring of the sprite
            Vector2 localScale = transform.localScale; // Creates a modifieable copy of transform from the inspector
            localScale.x *= -1f; // Updates the JUST the value
            transform.localScale = localScale; // Applying the effect to the gameobject
        }
    }

     void FixedUpdate() // Smoother Movement for Left and Right (in order to evenly distribute the milliseconds per frame)
    {
        charac1RB.velocity = new Vector2(horizontal * speed, charac1RB.velocity.y); 
    }
        
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer); // Returns true or false
    }

    void OnDrawGizmosSelected() // Adds highlight markings to whatever unseen system range detector
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
        }
        else
        {
            Debug.Log("Gizmos don't exists");
        }
    }

}
