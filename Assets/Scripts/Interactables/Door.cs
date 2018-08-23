using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public Item key;
	public GameObject textMesh;
	private bool collided;

	private void Update()
	{
		if (collided)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (Player.player.HasItem(key))
				{
					if(this.GetComponent<TriggerConversation>() != null)
					{
						this.GetComponent<TriggerConversation>().OpenDoor();
					}
					Destroy(this.gameObject);

				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			textMesh.SetActive(true);

			if (Player.player.HasItem(key))
			{
				textMesh.GetComponent<TextMesh>().text = "Press E to open";
			}
			else
			{
				textMesh.GetComponent<TextMesh>().text = "Door is locked";
			}

			collided = true;
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			textMesh.SetActive(false);
			collided = false;
		}
	}
}
