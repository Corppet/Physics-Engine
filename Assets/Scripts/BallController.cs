using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    [HideInInspector] public static BallController instance { private set; get; }

    [Range(0f, 100f)]
    [SerializeField] private float rollingSpeed = 10f;

    private Rigidbody2D myRigidbody;

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

        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // roll the ball
        myRigidbody.AddTorque(-rollingSpeed);
    }
}
