using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	// The Asteroid's movement speed.
	public float speed = 1f;

	// The Asteroid's rotation speed.
	public float torque = 180f;

	void Update()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);
		transform.Rotate(Vector3.back * torque * Time.deltaTime);
	}
}
