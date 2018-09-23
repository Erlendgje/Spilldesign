using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public float speed;
	public ParticleSystem ps;
	public bool vision = false;
	private bool canMove = true;

	private void Start()
	{
		GameManager.gameManager.player.SetPlayerObject(this.gameObject, this);
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (vision)
			{
				ps.Play();
			}
		}
	}

	private void FixedUpdate()
	{
		if (canMove)
		{
			Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			this.GetComponent<Rigidbody>().velocity = movement * speed;
		}
	}

	public void setCanMove(bool canMove)
	{
		this.canMove = canMove;
		this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
	}
}