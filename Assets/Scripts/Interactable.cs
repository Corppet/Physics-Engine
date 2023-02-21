using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Interactable : MonoBehaviour
{
    [Header("Camera Bounds")]
    [Range(-1f, 1f)]
    [SerializeField] private float leftBoundScale = -1f;
    [Range(-1f, 1f)]
    [SerializeField] private float rightBoundScale = 1f;
    [Range(-1f, 1f)]
    [SerializeField] private float topBoundScale = 1f;
    [Range(-1f, 1f)]
    [SerializeField] private float bottomBoundScale = -1f;

    private float leftBound;
    private float rightBound;
    private float topBound;
    private float bottomBound;

    private Rigidbody2D myRB;

    private void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Camera camera = Camera.main;
        float width = camera.orthographicSize * camera.aspect;
        float height = camera.orthographicSize;

        leftBound = camera.ViewportToWorldPoint(new Vector3(leftBoundScale * width, 0f, 0f)).x;
        rightBound = camera.ViewportToWorldPoint(new Vector3(rightBoundScale * width, 0f, 0f)).x;
        topBound = camera.ViewportToWorldPoint(new Vector3(0f, topBoundScale * height, 0f)).y;
        bottomBound = camera.ViewportToWorldPoint(new Vector3(0f, bottomBoundScale * height, 0f)).y;

        // move the object if out of bounds
        if (transform.position.x < leftBound)
        {
            myRB.MovePosition(new Vector2(leftBound, transform.position.y));
        }
        else if (transform.position.x > rightBound)
        {
            myRB.MovePosition(new Vector2(rightBound, transform.position.y));
        }
        
        if (transform.position.y < bottomBound)
        {
            myRB.MovePosition(new Vector2(transform.position.x, bottomBound));
        }
        else if (transform.position.y > topBound)
        {
            myRB.MovePosition(new Vector2(transform.position.x, topBound));
        }
    }
}
