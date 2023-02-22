using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [HideInInspector] public static CursorController Instance { get; private set; }

    [HideInInspector] public Rigidbody2D selectedRB;

    [Range(0f, 100f)]
    [SerializeField] private float maxForce = 10f;
    [Range(0f, 100f)]
    [SerializeField] private float maxSpeed = 10f;

    private Vector3 cursorPosition;
    private Vector2 cursorForce;
    private Vector3 lastPosition;
    private float originalRotation;
    private Vector3 offset;

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

        selectedRB = null;
    }

    private void Update()
    {
        if (!GameManager.instance.isInPlay)
            return;

        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // select and "pick up" an object with the cursor
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(cursorPosition);
            if (targetObject && targetObject.CompareTag("Interactable"))
            {
                selectedRB = targetObject.GetComponent<Rigidbody2D>();
                selectedRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                originalRotation = selectedRB.rotation;
                offset = selectedRB.transform.position - cursorPosition;
            }
        }
        // move the selected object with the cursor
        else if (selectedRB)
        {
            cursorForce = (cursorPosition - lastPosition) / Time.deltaTime;
            cursorForce = Vector2.ClampMagnitude(cursorForce, maxForce);
            lastPosition = cursorPosition;

            //selectedRigidbody.rotation = originalRotation - cursorForce.x * 2f;

            // release the selected object
            if (Input.GetMouseButtonUp(0))
            {
                selectedRB.velocity = Vector2.zero;
                selectedRB.AddForce(cursorForce, ForceMode2D.Impulse);
                selectedRB.constraints |= RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                selectedRB = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (selectedRB)
        {
            selectedRB.MovePosition(Vector2.MoveTowards(selectedRB.position, cursorPosition + offset, maxSpeed * Time.fixedDeltaTime));
        }
    }
}
