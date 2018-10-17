using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;

	public Player player;

	public Text addItemText;
	private float fade = 1;

	private void Awake()
	{
		if (gameManager == null)
		{
			gameManager = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
		player = new Player();
	}

	private void Start()
	{
		DontDestroyOnLoad(this);
        DontDestroyOnLoad(Camera.main);
	}

	public void AddItem(Item item)
	{
		player.AddItem(item);
		if(item as Upgrade)
		{
			addItemText.text = "Drinking " + item.itemName;
		}
		else
		{
			addItemText.text = "You picked up " + item.itemName;
		}
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
