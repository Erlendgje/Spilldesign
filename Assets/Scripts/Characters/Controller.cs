using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Character
{
	public float speed;
	public ParticleSystem ps;
	public ParticleSystem psPassive;
    private bool psPassiveActive = false;
	private bool canMove = true;
    private float cd = 0.5f;
    private float cdRemaining;

	private void Start()
	{
        base.Start();
		GameManager.gameManager.player.SetPlayerObject(this.gameObject, this);
        DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update()
	{
        Camera.main.transform.position = new Vector3(this.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        if(cdRemaining > 0)
        {
            cdRemaining -= Time.deltaTime;
        }

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Player.vision && cdRemaining <= 0)
			{
				ps.Play();
                cdRemaining = cd;
			}
		}

        if(this.GetComponent<Rigidbody>().velocity.x != 0 || this.GetComponent<Rigidbody>().velocity.y != 0)
        {
            if (!psPassiveActive)
            {
                psPassive.Play();
                psPassiveActive = true;
            }
        }
        else
        {
            psPassive.Stop();
            psPassiveActive = false;
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