using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Observer {

	public Item key;
	public GameObject textMesh;
	public GameObject parent;
	private bool collided;
	public bool canBeUnlocked;

	private void Update()
	{
		if (canBeUnlocked)
		{
			if (collided)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (GameManager.gameManager.player.HasItem(key))
					{
						if (this.GetComponent<TriggerConversation>() != null)
						{
							this.GetComponent<TriggerConversation>().OpenDoor();
						}
						Destroy(parent);
					}
				}
			}
		}
		else
		{

		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (canBeUnlocked)
		{
			if (other.CompareTag("Player"))
			{
				textMesh.SetActive(true);
				textMesh.transform.eulerAngles = new Vector3(0, 0, 0);

				if (GameManager.gameManager.player.HasItem(key))
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
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			textMesh.SetActive(false);
			collided = false;
		}
	}

	public void Notify()
	{
		canBeUnlocked = true;
	}
}
