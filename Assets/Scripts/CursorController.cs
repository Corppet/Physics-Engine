using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get; private set; }

    [HideInInspector] public Interactable selectedPlatform;
    [HideInInspector] public Vector3 offset { get; private set; }

    [Range(0f, 100f)]
    public float maxForce = 10f;
    [Range(0f, 100f)]
    public float maxSpeed = 10f;

    private Vector3 cursorPosition;
    private Vector2 cursorForce;
    private Vector3 lastPosition;

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

        selectedPlatform = null;
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
                targetObject.GetComponent<Interactable>().Select(this); 
                offset = selectedPlatform.transform.position - cursorPosition;
            }
        }
        // move the selected object with the cursor
        else if (selectedPlatform)
        {
            cursorForce = (cursorPosition - lastPosition) / Time.deltaTime;
            cursorForce = Vector2.ClampMagnitude(cursorForce, maxForce);
            lastPosition = cursorPosition;
    
            // release the selected object
            if (Input.GetMouseButtonUp(0))
            {
                selectedPlatform.Deselect(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if (selectedPlatform)
        {
            selectedPlatform.MoveToCursor(this, Time.fixedDeltaTime);
        }
    }
}
