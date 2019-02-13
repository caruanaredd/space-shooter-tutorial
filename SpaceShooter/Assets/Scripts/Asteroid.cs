﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code enforces the use of a Rigidbody2D component.
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
	// The Asteroid's movement speed.
	public float speed = 1f;

	// The Asteroid's rotation speed.
	public float torque = 180f;

	// The Asteroid's Rigidbody2D component
	private Rigidbody2D _asteroidRB;

	// Any components we'll need to reference later on,
	// should be acquired when the script wakes up.
	private void Awake()
	{
		// We're looking for the Rigidbody2D component
		// on this object, and taking reference.
		_asteroidRB = GetComponent<Rigidbody2D>();
	}

	// When the game/scene starts (after Awake), this
	// is what it will do.
	private void Start()
	{
		_asteroidRB.AddForce(Random.insideUnitCircle * speed,
			ForceMode2D.Impulse);
		
		_asteroidRB.AddTorque((Random.value - 0.5f) * torque,
			ForceMode2D.Impulse);
	}
}
