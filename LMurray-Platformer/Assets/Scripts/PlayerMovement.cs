using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;

    [SerializeField] private float xSpeed = 10f;
    [SerializeField] private float jumpForce = 800f;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float wallCheckRadius = 1f;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private AudioSource jumpSoundEffect;
    //[SerializeField] private AudioSource landSoundEffect;

    private bool isWalled;
    private bool isWallSliding;
    private float wallSlideSpeed;

    private bool doubleJump;
    private float doubleJumpPower = 12f;
    private int maxJumps = 2;
    private int jumpsRemaining;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    private bool _facingRight = true;
    public float XSpeed => xSpeed;
    private Rigidbody2D _rb;
    private float xMoveInput;
    private bool _shouldJump;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;
    public bool IsWalled => isWalled;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "FutureScene")
        {
            jumpsRemaining = maxJumps;
        }
    }

    void Update()
    {
        if (!isWallJumping)
        {
            if (_facingRight && _rb.velocity.x < -0.1)
            {
                Flip();
            }
            else if (!_facingRight && _rb.velocity.x > 0.1)
            {
                Flip();
            }
        }

        xMoveInput = Input.GetAxis("Horizontal") * xSpeed;
        if (SceneManager.GetActiveScene().name == "FutureScene") 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _shouldJump = true;
            }
        }

        WallSlide();
        WallJump();
    }

    private void FixedUpdate()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        Collider2D col2 = Physics2D.OverlapCircle(transform.position, wallCheckRadius, wallLayer);
        _isGrounded = col != null;
        isWalled = col2 != null;
        _rb.velocity = new Vector2(xMoveInput, _rb.velocity.y);
        if (doubleJump)
        {
            if (_isGrounded || doubleJump && jumpsRemaining > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, doubleJump ? doubleJumpPower : jumpForce);
                //_rb.AddForce(Vector2.up * jumpForce);
                jumpSoundEffect.Play();
                jumpsRemaining--;

                doubleJump = !doubleJump;
            }
            _shouldJump = false;
        }
        if (_isGrounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
            jumpsRemaining = maxJumps;
        }
        if (_shouldJump)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector2.up * jumpForce);
                jumpSoundEffect.Play();
            }
            _shouldJump = false;
        }
       
    }

    private void WallSlide()
    {
        if (isWalled && !_isGrounded && xMoveInput != 0f)
        {
            isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding) 
        {
            isWallJumping = false;
            wallJumpingDirection = transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            _rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

           /* if (transform.localScale.x != wallJumpingDirection)
            {
                _facingRight = !_facingRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }*/
        }
        Invoke(nameof(StopWallJumping), wallJumpingDuration);
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform, true);
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null, true);
        }

    }
}
