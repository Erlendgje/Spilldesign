using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

	public float speed;
	public ParticleSystem ps;

	

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			ps.Play();
		}

	}

	private void FixedUpdate()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		this.GetComponent<Rigidbody>().velocity = movement * speed;
	}

}