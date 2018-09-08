using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour {

	private float time = 0.05f;

	public Dialogue dialogue;
	public TextMesh textMesh;
	public GameObject infoMesh;
	public GameObject textBobble;
	private float rowLimit = 1f;
	private int pointInConversation = 0;
	private bool isRenderingDialog = false;
	private bool writingText;
	public List<Door> observers;

	public void activate()
	{
		StartCoroutine(StartConversation());
	}

	public IEnumerator StartConversation()
	{
		yield return new WaitWhile(() => isRendering());
		isRenderingDialog = true;
		for (int i = 0; i < dialogue.conversations[pointInConversation].messages.Length; i++)
		{
			StartCoroutine(DisplayText(dialogue.conversations[pointInConversation].messages[i].line, false));

			yield return new WaitWhile(() => writingText);
			if (dialogue.conversations[pointInConversation].messages[i].pressE)
			{
				infoMesh.SetActive(true);
				yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));
				infoMesh.SetActive(false);
			}

			if(dialogue.conversations[pointInConversation].messages[i].item != null)
			{
				GameManager.gameManager.AddItem(dialogue.conversations[pointInConversation].messages[i].item);
			}

			if (dialogue.conversations[pointInConversation].messages[i].guiding)
			{
				StartCoroutine(GetComponent<Guiding>().GuidePlayer());
				yield return new WaitUntil(() => GetComponent<Guiding>().isPickedUp());
			}
		}

		if (dialogue.conversations[pointInConversation].accessDoors)
		{
			notifyObservers();
		}

		if (dialogue.conversations[pointInConversation].dissapear)
		{
			yield return new WaitForSeconds(dialogue.conversations[pointInConversation].time);
			textBobble.SetActive(false);
		}
		if (dialogue.disappear)
		{
			if(pointInConversation + 1 == dialogue.conversations.Length)
			{
				Destroy(this.gameObject);
			}
		}
		NextConversation();
		isRenderingDialog = false;
	}


	public IEnumerator DisplayText(string text, bool instant)
	{
		writingText = true;
		textBobble.SetActive(true);
		string builder = "";
		textMesh.text = "";
		string[] parts = text.Split(' ');
		for(int i = 0; i < parts.Length; i++)
		{
			char[] subParts = parts[i].ToCharArray();
			for(int k = 0; k < subParts.Length; k++)
			{
				textMesh.text += subParts[k];
				if (!instant)
				{
					yield return new WaitForSeconds(time);
				}
			}

			textMesh.text += ' ';
			
			if (textMesh.GetComponent<Renderer>().bounds.extents.x > rowLimit)
			{
				textMesh.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + ' ';
			}
			builder = textMesh.text;
		}
		writingText = false;
	}

	public void NextConversation()
	{
		pointInConversation++;
	}

	public bool isRendering()
	{
		return isRenderingDialog;
	}

	public void deactivateTextBubble()
	{
		textBobble.SetActive(false);
	}

	private void notifyObservers()
	{
		foreach(Door d in observers)
		{
			d.Notify();
		}
	}
}
