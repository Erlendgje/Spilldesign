using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guiding : MonoBehaviour {

	public Directions directions;
	private DialogueHandler dh;
	private GameObject player;
	public GameObject destination;
	private bool pickedUp = false;
	private static float DISTANCE = 0.5f;

	// Use this for initialization
	void Start () {
		dh = this.GetComponent<DialogueHandler>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public IEnumerator GuidePlayer()
	{
		while(destination != null)
		{
			if(Vector3.Distance(player.transform.position, destination.transform.position) < DISTANCE)
			{
				StartCoroutine(dh.DisplayText(directions.arrived, true));
			}
			else
			{
				StartCoroutine(dh.DisplayText(generateDirection(), true));
			}
			yield return new WaitForSeconds(0);
		}
		pickedUp = true;
		dh.deactivateTextBubble();
		
	}

	private string generateDirection()
	{
		string text = directions.direction;
		bool and = false;

		if (player.transform.position.y - DISTANCE > destination.transform.position.y)
		{
			text += " down";
			and = true;
		}
		else if (player.transform.position.y + DISTANCE < destination.transform.position.y)
		{
			text += " up";
			and = true;
		}

		if (player.transform.position.x - DISTANCE > destination.transform.position.x)
		{
			if (and)
			{
				text += " and";
			}
			text += " left";
		}
		else if (player.transform.position.x + DISTANCE < destination.transform.position.x)
		{
			if (and)
			{
				text += " and";
			}
			text += " right";
		}

		return text;
	}

	public bool isPickedUp()
	{
		return pickedUp;
	}
}
