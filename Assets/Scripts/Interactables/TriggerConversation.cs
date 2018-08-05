using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConversation : MonoBehaviour {

	public DialogueHandler character;
	public bool isDoor = false;

	public void OpenDoor()
	{
		character.activate();
		Destroy(this.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !isDoor)
		{
			character.activate();
			Destroy(this.gameObject);
		}
	}
}
