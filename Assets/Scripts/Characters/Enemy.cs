using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{

	public List<GameObject> path;
	public bool loop;
	public ParticleSystem ps;
	public GameObject trigger;

	public bool moving = true;
	private float movementSpeed = 1;
	private float rotationSpeed = 5;

	private int index = 0;
	private int modifier = 1;

	public bool spotted = false;

	// Use this for initialization
	void Start()
	{
		base.Start();
		if (path.Count != 0) {
			ps.Play();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (path.Count != 0 && moving) {

			Vector2 from = this.transform.position;
			Vector2 to = path[index].transform.position;
			transform.position = Vector2.MoveTowards(from, to, movementSpeed * Time.deltaTime);
			Vector2 vectorToTarget = to - from;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle - 90, new Vector3(0, 0, 1));
			if (trigger != null) {
				trigger.transform.rotation = Quaternion.Slerp(trigger.transform.rotation, q, Time.deltaTime * rotationSpeed);
			}
			if (Vector3.Distance(transform.position, path[index].transform.position) < 0.1) {
				index += modifier;
				if (index >= path.Count || index < 0) {
					if (loop) {
						index = 0;
					}
					else {
						modifier = modifier * -1;
						index += modifier;
					}
				}
			}
		}
	}

	private void OnDestroy()
	{
		if (spotted) {
			int index = SceneManager.GetActiveScene().buildIndex;
			SceneManager.UnloadSceneAsync(index);
			GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("Checkpoint").transform.position;
			GameManager.gameManager.player.canMove(true);
			SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

		}
	}
}