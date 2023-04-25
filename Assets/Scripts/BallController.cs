using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    [HideInInspector] public static BallController instance { private set; get; }

    [Range(0f, 360f)]
    [SerializeField] private float angularAcceleration = 10f;
    [Range(0f, 3600f)]
    [SerializeField] private float maxAngularVelocity = 720f;
    [Range(0f, 100f)]
    [SerializeField] private float maxSpeed = 10f;

    private Rigidbody2D body;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isInPlay)
            return;

        // roll the ball
        body.AddTorque(-angularAcceleration * body.inertia);
        
        // clamp the ball's rolling speed
        if (body.angularVelocity > maxAngularVelocity)
        {
            body.angularVelocity = maxAngularVelocity;
        }
        else if (body.angularVelocity < -maxAngularVelocity)
        {
            body.angularVelocity = -maxAngularVelocity;
        }

        // clamp the ball's speed
        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
        else if (body.velocity.magnitude < -maxSpeed)
        {
            body.velocity = body.velocity.normalized * -maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the ball hits the ground or an obstacle, game over
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
