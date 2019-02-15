using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    // All colliders inherit from the same class,
    // which also has the same properties.
    private Collider2D _collider;

    // Every object has a radius, and it will help us
    // move the object only when it is out of view.
    private float _radius = 0f;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        
        // If the collider couldn't be found/doesn't exist,
        // the script will be turned off.
        if (_collider == null)
        {
            enabled = false;
            return;
        }

        _radius = _collider.bounds.extents.y;
    }

    // This method will work when the camera AND scene
    // lose sight of the object.
    private void OnBecameInvisible()
    {
        // Handles the object when it exits from the left.
        if (transform.position.x < CameraBounds.bottomLeft.x)
        {
            transform.position = new Vector2(
                CameraBounds.topRight.x + _radius, transform.position.y);
        }
        // Handles the object when it exits from the right.
        else if (transform.position.x > CameraBounds.topRight.x)
        {
            transform.position = new Vector2(
                    CameraBounds.bottomLeft.x - _radius, transform.position.y);
        }

        // Handles the object when it exits from the bottom.
        if (transform.position.y < CameraBounds.bottomLeft.y)
        {
            transform.position = new Vector2(
                transform.position.x, CameraBounds.topRight.y + _radius);
        }
        // Handles the object when it exits from the top.
        else if (transform.position.y > CameraBounds.topRight.y)
        {
            transform.position = new Vector2(
                    transform.position.x, CameraBounds.bottomLeft.y - _radius);
        }
    }
}
