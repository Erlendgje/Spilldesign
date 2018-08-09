using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float speed;
	public ParticleSystem ps;

	private void Start()
	{
		Player.player.SetPlayerObject(this.gameObject);
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			ps.Play();
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			GameManager.gameManager.ActivateInventory();
		}
	}

	private void FixedUpdate()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		this.GetComponent<Rigidbody>().velocity = movement * speed;
	}
}