using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private float h;
    public float speed;
    private Rigidbody2D rb;

    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;
    bool jump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(1.7f, 0.24f), 0, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);

        if (jump)
        {
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        jump = false;
    }
}
