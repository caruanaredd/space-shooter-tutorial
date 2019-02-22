using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // This is the player's health.
    [Range(0f, 100f)]
    public float health = 100f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        // We'll only damage the player if we hit an asteroid.
        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroids"))
        {
            // Since we've collided with an asteroid,
            // we should have its script available.

            // <Asteroid> is used to enforce the type of script we need.
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
            health -= asteroid.damage;

            asteroid.Break();
        }
    }
}
