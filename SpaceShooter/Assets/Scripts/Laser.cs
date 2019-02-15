using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Laser : MonoBehaviour
{
    // Controls the lasers' speed.
    public float speed = 10f;

    // A reference to the laser's rigidbody.
    private Rigidbody2D _bulletRB;

    private void Awake()
    {
        _bulletRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _bulletRB.velocity =
            transform.TransformDirection(Vector2.up) * speed;
    }
}
