using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

	public Item item;
	public Text text;

	public void OnPointerClick(PointerEventData eventData)
	{
		GameManager.gameManager.SetActiveItem(this);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{

		this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
		text.color = new Color(0,0,0,1);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		this.GetComponent<Image>().color = new Color(0, 0, 0, 1);
		text.color = new Color(1, 1, 1, 1);
	}

	// Use this for initialization
	public void setItem (Item item) {
		this.item = item;
		text.text = item.itemName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
