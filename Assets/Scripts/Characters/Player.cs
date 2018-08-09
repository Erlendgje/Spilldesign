using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	public static Player player = new Player();

	private List<Item> inventory;

	private GameObject playerObject;

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

	public void SetPlayerObject(GameObject player)
	{
		playerObject = player;
	}

	public Transform GetTransform()
	{
		return playerObject.transform;
	}

	public void RemoveItem(Item item)
	{
		inventory.Remove(item);
	}
}
