using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Laser : MonoBehaviour
{
    // Controls the lasers' speed.
    public float speed = 10f;

    // The laser's lifetime.
    public float lifetime = 2f;

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
        
        // Destroy this object after a specific amount of time.
        Destroy(gameObject, lifetime);
    }

    // When the last collides with other objects, we can
    // handle what happens in the scene.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroids"))
        {
            Destroy(other.gameObject);
        }

        // The laser should be destroyed if it hits anything.
        Destroy(gameObject);
    }
}
