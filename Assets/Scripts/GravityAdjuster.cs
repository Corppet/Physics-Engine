using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityAdjuster : MonoBehaviour
{
    [Tooltip("The new gravity scale to apply to the player when they enter this trigger.")]
    [SerializeField] private float newGravityScale = 9.81f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 direction = newGravityScale * transform.up;
            GameManager.Instance.SetGravity(direction);
            Destroy(gameObject);
        }
    }
}
