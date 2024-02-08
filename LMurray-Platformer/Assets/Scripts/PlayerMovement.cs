using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float xSpeed = 10f;
    [SerializeField] private float jumpForce = 800f;
    private Rigidbody2D _rb;
    private float xMoveInput;
    private bool _shouldJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMoveInput = Input.GetAxis("Horizontal") * xSpeed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shouldJump = true;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(xMoveInput, _rb.velocity.y);
        if (_shouldJump)
        {
            _rb.AddForce(Vector2.up * jumpForce);
            _shouldJump = false;
        }
    }
}
