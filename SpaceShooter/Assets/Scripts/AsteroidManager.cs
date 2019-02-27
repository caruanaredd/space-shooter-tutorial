using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    // Static variables are available from all ends of the code.
    // get -> any class can read this variable.
    // private set -> only this class can set the variable.
    public static AsteroidManager main { get; private set; }

    // The asteroid templates.
    public Asteroid[] bigAsteroids, mediumAsteroids, smallAsteroids, tinyAsteroids;

    // The number of asteroids to generate when the game starts.
    public int numAsteroids = 4;

    // The number of children to spawn when the asteroid is destroyed.
    public int numChildren = 3;

    // The exclude radius when generating the first set.
    public float excludeRadius = 3f;

    private void Awake()
    {
        if (main == null)
        {
            // if there is no main object, this will take its place.
            main = this;
        }
        else
        {
            // if a main object exists, we'll self-destruct.
            DestroyImmediate(this);
        }
    }

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

    // Will spawn a number of children asteroids in a specified position.
    public void GenerateChildren(int size, Vector2 position, Vector2 radius)
    {
        // check if the size is zero, if so, stop here.
        if (size <= 0) return;

        // this is where we'll store the asteroids to generate.
        Asteroid[] asteroids = new Asteroid[]{ };

        // Use the next size asteroid from the one we have.
        switch (size)
        {
            case 3:
                asteroids = mediumAsteroids;
                break;

            case 2:
                asteroids = smallAsteroids;
                break;

            case 1:
                asteroids = tinyAsteroids;
                break;

            default:
                return;
        }

        // Generate the number of asteroids this class has stored.
        for (int i = 0; i < numChildren; i++)
        {
            int index = Random.Range(0, asteroids.Length);
            Asteroid asteroid = Instantiate<Asteroid>(asteroids[index], transform);
            asteroid.transform.position = position + Random.insideUnitCircle * radius;
        }
    }
}
