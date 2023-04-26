using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAxisPlatform : Interactable
{
    public enum MoveDirection
    {
        Horizontal,
        Vertical
    }

    public MoveDirection direction;

    public override void Select(CursorController cursor)
    {
        base.Select(cursor);
        switch (direction)
        {
            case MoveDirection.Horizontal:
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                break;
            case MoveDirection.Vertical:
                rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                break;
        }
    }
}
