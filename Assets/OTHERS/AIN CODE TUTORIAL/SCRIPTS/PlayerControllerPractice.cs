using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPractice : MonoBehaviour
{
    // --------------- VARIABLES ---------------

    [Header("REFERENCE")]
    [SerializeField] Rigidbody2D playerRB;

    [Header("ATTRIBUTES")]
    [SerializeField] float speed = 5f;
    [SerializeField] float vertical;
    [SerializeField] float horizontal;

    // --------------- UNITY METHODS ---------------

    // ...
    void Awake()
    {
        if (playerRB == null)
            playerRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }


    }

    // --------------- CUSTOM METHODS ---------------

    public void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        playerRB.velocity = new Vector2(horizontal * speed, playerRB.velocity.y);
    }

    public void Jump()
    {
        
    }
}