using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text addItemText;

	private float fade = 1;

	public void AddItem(Item item)
	{
		Player.player.AddItem(item);
		addItemText.text = item.itemName + " added to inventory";
		StartCoroutine(WaitForFade(1.5f, addItemText));
	}

	private IEnumerator WaitForFade(float seconds, Text text)
	{
		text.color = new Vector4(text.color.r, text.color.g, text.color.b, 1);
		yield return new WaitForSeconds(seconds);
		StartCoroutine(FadeText(text));
	}

	private IEnumerator FadeText(Text text)
	{
		while(text.color.a > 0)
		{
			text.color = new Vector4(text.color.r,text.color.g,text.color.b,text.color.a - (fade * Time.deltaTime));
			yield return new WaitForSeconds(0);
		}
	}

}
