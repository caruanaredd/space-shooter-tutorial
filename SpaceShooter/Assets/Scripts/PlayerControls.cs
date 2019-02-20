using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
// A class for a Unity GameObject extends from MonoBehaviour.
// This will enable code to control an object in the scene.
public class PlayerControls : MonoBehaviour
{
	// The player's movement speed.
	public float speed = 10f;

	// The player's rotation speed.
	public float torque = 360f;

	// This is the location where the laser will be spawned.
	public Transform laserTurret;

	// This is the prefab/template for the laser.
	public GameObject laser;

	// A reference to the Rigidbody2D component.
	private Rigidbody2D _playerRB;

	// We'll keep the direction as a separate variable.
	private Vector2 _direction;

	private void Awake()
	{
		_playerRB = GetComponent<Rigidbody2D>();
	}

	// This function will execute for every frame.
	void Update()
	{
		// Will read the input on the Up/Down or W/S keys.
		_direction = transform.TransformDirection(Vector2.up)
			*  Input.GetAxis("Vertical");

		// Will read the input on the Left/Right or A/D keys.
		float horz = Input.GetAxis("Horizontal");

		// Will rotate the player.
		transform.Rotate(Vector3.back * horz * torque * Time.deltaTime);

		// If the space key was pressed.
		if (Input.GetButtonDown("Submit"))
		{
			// Create a copy/instance of the laser.
			Instantiate(laser, laserTurret.position, laserTurret.rotation);
		}
	}

	// Use this every time we move things via Rigidbody
	private void FixedUpdate()
	{
		_playerRB.MovePosition(_playerRB.position
			+ _direction * speed * Time.deltaTime);
	}
}
