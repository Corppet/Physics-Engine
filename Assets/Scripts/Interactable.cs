using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Interactable : MonoBehaviour
{
    #region Camera Bounds

    //[Header("Camera Bounds")]
    //[Range(-1f, 1f)]
    //[SerializeField] protected float leftBoundScale = -1f;
    //[Range(-1f, 1f)]
    //[SerializeField] protected float rightBoundScale = 1f;
    //[Range(-1f, 1f)]
    //[SerializeField] protected float topBoundScale = 1f;
    //[Range(-1f, 1f)]
    //[SerializeField] protected float bottomBoundScale = -1f;

    //protected float leftBound;
    //protected float rightBound;
    //protected float topBound;
    //protected float bottomBound;

    #endregion

    new protected Rigidbody2D rigidbody;

    public virtual void Select(CursorController cursor)
    {
        cursor.selectedPlatform = this;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public virtual void Deselect(CursorController cursor)
    {
        cursor.selectedPlatform = null;
        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    public virtual void MoveToCursor(CursorController cursor, float deltaTime)
    {
        rigidbody.MovePosition(Vector2.MoveTowards(transform.position, cursor.transform.position + cursor.offset,
            cursor.maxSpeed * deltaTime));
    }

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        #region Camera Bounds

        //Camera camera = Camera.main;
        //float width = camera.orthographicSize * camera.aspect;
        //float height = camera.orthographicSize;

        //leftBound = camera.ViewportToWorldPoint(new Vector3(leftBoundScale * width, 0f, 0f)).x;
        //rightBound = camera.ViewportToWorldPoint(new Vector3(rightBoundScale * width, 0f, 0f)).x;
        //topBound = camera.ViewportToWorldPoint(new Vector3(0f, topBoundScale * height, 0f)).y;
        //bottomBound = camera.ViewportToWorldPoint(new Vector3(0f, bottomBoundScale * height, 0f)).y;

        //// move the object if out of bounds
        //if (transform.position.x < leftBound)
        //{
        //    rigidbody.MovePosition(new Vector2(leftBound, transform.position.y));
        //}
        //else if (transform.position.x > rightBound)
        //{
        //    rigidbody.MovePosition(new Vector2(rightBound, transform.position.y));
        //}
        
        //if (transform.position.y < bottomBound)
        //{
        //    rigidbody.MovePosition(new Vector2(transform.position.x, bottomBound));
        //}
        //else if (transform.position.y > topBound)
        //{
        //    rigidbody.MovePosition(new Vector2(transform.position.x, topBound));
        //}

        #endregion
    }
}
