using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    public static BallController Instance { private set; get; }

    [Range(0f, 360f)]
    public float angularAcceleration = 10f;
    [Range(0f, 3600f)]
    public float maxAngularSpeed = 720f;
    [Range(0f, 100f)]
    public float maxSpeed = 10f;

    new private Rigidbody2D rigidbody;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isInPlay)
            return;

        // roll the ball
        rigidbody.AddTorque(-angularAcceleration * rigidbody.inertia);
        
        // clamp the ball's rolling speed
        rigidbody.angularVelocity = Mathf.Clamp(rigidbody.angularVelocity, -maxAngularSpeed, maxAngularSpeed);

        // clamp the ball's speed
        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
        }
        else if (rigidbody.velocity.magnitude < -maxSpeed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * -maxSpeed;
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
