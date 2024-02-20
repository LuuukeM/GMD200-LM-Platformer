using UnityEngine;

public class DirtyBubbleScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionInterval = 3f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float nextDirectionChangeTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextDirectionChangeTime = Time.deltaTime + Random.Range(0f, changeDirectionInterval);
        CalculateNewMoveDirection();
    }

    void Update()
    {
        if (Time.time >= nextDirectionChangeTime)
        {
            CalculateNewMoveDirection();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void CalculateNewMoveDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = Random.insideUnitCircle.normalized;
    }
}
