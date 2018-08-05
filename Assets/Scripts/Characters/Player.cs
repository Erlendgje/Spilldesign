using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	public static Player player = new Player();

	private List<Item> inventory;

	public GameObject UIManager;

	private Player()
	{
		inventory = new List<Item>();
	}

	public bool HasItem(Item item)
	{
		return inventory.Contains(item);
	}

	public void AddItem(Item item)
	{
		inventory.Add(item);
	}

}
