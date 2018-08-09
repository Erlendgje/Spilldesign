using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager gameManager;
	public GameObject inventory;
	public GameObject inventoryList;
	public GameObject itemElementUI;
	public Text itemName;
	public Text itemDescription;

	public GameObject itemPrefab;

	private ItemElement activeItemElement;


	public Text addItemText;
	private float fade = 1;

	private void Start()
	{
		if (gameManager == null)
		{
			gameManager = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public void AddItem(Item item)
	{
		Player.player.AddItem(item);
		activeItemElement = Instantiate(itemElementUI, inventoryList.transform.position, inventoryList.transform.rotation, inventoryList.transform).GetComponent<ItemElement>();
		activeItemElement.setItem(item);
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

	public void ActivateInventory()
	{
		if (inventory.activeInHierarchy)
		{
			inventory.SetActive(false);
		}
		else
		{
			inventory.SetActive(true);
		}
		
	}

	public void SetActiveItem(ItemElement item)
	{
		activeItemElement = item;
		itemName.text = item.item.itemName;
		itemDescription.text = item.item.description;
	}

	public void Equip()
	{
		if(activeItemElement != null)
		{

		}
	}

	public void Eat()
	{
		if (activeItemElement != null)
		{

		}
	}

	public void Drop()
	{
		if (activeItemElement != null)
		{
			Instantiate(itemPrefab, Player.player.GetTransform().position, Player.player.GetTransform().rotation).GetComponent<ItemScript>().item = activeItemElement.item;
			Player.player.RemoveItem(activeItemElement.item);
			Destroy(activeItemElement.gameObject);
			itemName.text = "";
			itemDescription.text = "";
		}
	}

}
