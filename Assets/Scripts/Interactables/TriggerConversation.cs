using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConversation : MonoBehaviour {

	public DialogueHandler character;
	public bool isDoor = false;
	public bool dontDestroy = false;
	public bool activateEvenIfActive;

	public void OpenDoor()
	{
		character.activate(activateEvenIfActive);
		Destroy(this.gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !isDoor)
		{
			character.activate(activateEvenIfActive);

			if (!dontDestroy)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
