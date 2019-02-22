using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // The asteroid templates.
    public Asteroid[] bigAsteroids;

    // The number of asteroids to generate when the game starts.
    public int numAsteroids = 4;

    // The exclude radius when generating the first set.
    public float excludeRadius = 3f;

    private void Start()
    {
        GenerateBigAsteroids();
    }

    // Will generate the biggest asteroids using the bigAsteroids array.
    public void GenerateBigAsteroids()
    {
        for (int i = 0; i < numAsteroids; i++)
        {
            // First, we'll choose a random item from the array.
            int index = Random.Range(0, bigAsteroids.Length);

            // Generate the asteroid without a position/rotation.
            Asteroid asteroid = Instantiate<Asteroid>(bigAsteroids[index], transform);

            // Generate a Vector2 position at random.
            Vector2 pos = Random.insideUnitCircle;

            // Stretch the vector to match the width/height of the screen.
            pos.x *= CameraBounds.topRight.x;
            pos.y *= CameraBounds.topRight.y;

            // If the asteroid is closer than the radius, we'll push it away.
            // Mathf.Abs -> turn any number into a positive value.
            // Mathf.Sign -> will give us -1 for negative, 1 for positive values.
            if (Mathf.Abs(pos.x) < excludeRadius)
                pos.x += Mathf.Sign(pos.x) * excludeRadius;

            if (Mathf.Abs(pos.y) < excludeRadius)
                pos.y += Mathf.Sign(pos.y) * excludeRadius;
            
            // Move the asteroid to this new position.
            asteroid.transform.position = Random.insideUnitCircle.normalized * pos;
        }
    }
}
