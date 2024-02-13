using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Transform[] movePoints;

    private int _pointIndex = 0;
    private Transform _currentPoint;

    void Start()
    {
        _currentPoint = movePoints[_pointIndex];
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentPoint.position, moveSpeed);
        if (Vector2.Distance(transform.position, _currentPoint.position)<0.01f)
        {
            _pointIndex++;
            _pointIndex%= movePoints.Length;
            _currentPoint = movePoints[_pointIndex];
        }
    }
}
