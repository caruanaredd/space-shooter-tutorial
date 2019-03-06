using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // This is the player's health.
    [Range(0f, 100f)]
    public float health = 100f;

    // We'll use a reference to the shield slider to show health.
    public ShieldSlider shieldSlider;

    // A reference to the shield gameobject on the scene.
    public GameObject shield;

    // How long will the shield be active for in seconds?
    public float shieldDuration = 1f;

    private void Start()
    {
        shieldSlider.SetValue(health);
        StartCoroutine(ActivateShield());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // We'll only damage the player if we hit an asteroid.
        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroids"))
        {
            // Since we've collided with an asteroid,
            // we should have its script available.

            // <Asteroid> is used to enforce the type of script we need.
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
            
            if (!shield.activeSelf)
            {
                Damage(asteroid.damage);
                asteroid.Break(false);
                StartCoroutine(ActivateShield());
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        shieldSlider.SetValue(health);
    }

    // IEnumerators are timers that occupy a different thread to the game.
    private IEnumerator ActivateShield()
    {
        shield.SetActive(true);

        // this function will wait for 1 second before doing the rest
        // of its commands.
        yield return new WaitForSeconds(shieldDuration);

        shield.SetActive(false);
    }
}
