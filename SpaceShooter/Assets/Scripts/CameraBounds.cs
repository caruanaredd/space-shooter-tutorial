using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBounds : MonoBehaviour
{
    // These two variables will be a reference
    // to the World Space edges of the camera.
    // They are accessible from every script:
    // CameraBounds.topLeft or CameraBounds.bottomRight
    public static Vector2 bottomLeft, topRight;

    private void Awake()
    {
        // Get the camera component for use.
        Camera cam = GetComponent<Camera>();

        // Set the coordinates.
        bottomLeft = cam.ViewportToWorldPoint(Vector3.zero);
        topRight = cam.ViewportToWorldPoint(Vector3.one);
    }
}
