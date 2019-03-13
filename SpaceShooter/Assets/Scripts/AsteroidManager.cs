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

    // The level data the manager will work with.
    private LevelSettings _settings;

    // The exclude radius when generating the first set.
    public float excludeRadius = 3f;

    // Used to keep a reference of all the Asteroids in the scene.
    // We'll pool the objects in a single list.
    private List<Asteroid> _asteroids = new List<Asteroid>();

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

    // Will generate the biggest asteroids using the bigAsteroids array.
    public void GenerateBigAsteroids()
    {
        for (int i = 0; i < _settings.numAsteroids; i++)
        {
            // First, we'll choose a random item from the array.
            int index = Random.Range(0, bigAsteroids.Length);

            // Generate the asteroid without a position/rotation.
            Asteroid asteroid = Instantiate<Asteroid>(bigAsteroids[index], transform);
            _asteroids.Add(asteroid);

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
        if (size <= _settings.minSize) return;

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
        for (int i = 0; i < _settings.numChildren; i++)
        {
            int index = Random.Range(0, asteroids.Length);
            Asteroid asteroid = Instantiate<Asteroid>(asteroids[index], transform);
            _asteroids.Add(asteroid);
            asteroid.transform.position = position + Random.insideUnitCircle * radius;
        }
    }

    // Will clear all the registered asteroids in the list.
    public void Clear()
    {
        foreach (Asteroid a in _asteroids)
        {
            Destroy(a.gameObject);
        }
        _asteroids.Clear();
    }

    // An asteroid can deregister itself from the list
    // to avoid removing objects that don't exist when
    // the game starts.
    public void DeregisterAsteroid(Asteroid asteroid)
    {
        _asteroids.Remove(asteroid);
        if (_asteroids.Count == 0)
        {
            GameManager.main.NextLevel();
        }
    }

    // Set the new level settings and generate the asteroids.
    public void SetSettings(LevelSettings settings)
    {
        _settings = settings;
        GenerateBigAsteroids();
    }
}
