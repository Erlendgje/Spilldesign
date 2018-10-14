using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character {

	public List<GameObject> path;
	public bool loop;
	public ParticleSystem ps;
	public GameObject trigger;

	private bool moving = true;
	private float movementSpeed = 1;
	private float rotationSpeed = 5;

	private int index = 0;
	private int modifier = 1;

	private bool spotted = false;

	// Use this for initialization
	void Start () {
        base.Start();
		if(path.Count != 0)
		{
			ps.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (path.Count != 0 && moving)
		{
			
			Vector2 from = this.transform.position;
			Vector2 to = path[index].transform.position;
			transform.position = Vector2.MoveTowards(from, to, movementSpeed * Time.deltaTime);
			Vector2 vectorToTarget = to - from;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle - 90, new Vector3(0, 0, 1));
			trigger.transform.rotation = Quaternion.Slerp(trigger.transform.rotation, q, Time.deltaTime * rotationSpeed);

			if (Vector3.Distance(transform.position, path[index].transform.position) < 0.1)
			{
				index += modifier;
				if (index >= path.Count || index < 0)
				{
					if (loop)
					{
						index = 0;
					}
					else
					{
						modifier = modifier * -1;
						index += modifier;
					}
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int layerMask = 1 << 9;
		layerMask = ~layerMask;

		RaycastHit hit;
		if(Physics.Raycast(transform.position, other.transform.position - transform.position, out hit, 10, layerMask))
		{
			if (hit.transform.gameObject.CompareTag("Player"))
			{
				spotted = true;
				GameManager.gameManager.player.canMove(false);
				moving = false;
				GetComponent<DialogueHandler>().activate(true);
			}
		}
	}

	private void OnDestroy()
	{
		if (spotted)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}
	}
}