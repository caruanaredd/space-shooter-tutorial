using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class for a Unity GameObject extends from MonoBehaviour.
// This will enable code to control an object in the scene.
public class PlayerControls : MonoBehaviour
{
	// The player's movement speed.
	public float speed = 10f;

	// The player's rotation speed.
	public float torque = 360f;

	// This function will execute for every frame.
	void Update()
	{
		// Will read the input on the Up/Down or W/S keys.
		float vert = Input.GetAxis("Vertical");

		// Will move the player vertically.
		transform.Translate(Vector3.up * vert * speed * Time.deltaTime);

		// Will read the input on the Left/Right or A/D keys.
		float horz = Input.GetAxis("Horizontal");

		// Will rotate the player.
		transform.Rotate(Vector3.back * horz * torque * Time.deltaTime);
	}
}
