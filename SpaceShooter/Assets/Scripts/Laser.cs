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

    // The audio source attached to this laser.
    private AudioSource _audioSource;

    private void Awake()
    {
        _bulletRB = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.pitch = Random.Range(0.7f, 1.3f);

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
            // <Asteroid> is used to enforce the type of script we need.
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();

            asteroid.Break();
        }

        // The laser should be destroyed if it hits anything.
        Destroy(gameObject);
    }
}
