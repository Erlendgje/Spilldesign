using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	public static Player player = new Player();

	private List<Item> inventory;

	private GameObject playerObject;

	private Eatable[] equipment;

	private int health = 1;
	private short numberOfParticles = 200;
	private float numberOfSeconds = 0.5f;

	private Player()
	{
		inventory = new List<Item>();
		equipment = new Eatable[4];
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

	public void EquipItem(Eatable item, int typeId)
	{
		if(equipment[typeId] != null)
		{
			health -= equipment[typeId].increaseHealth;
			numberOfParticles -= equipment[typeId].increaseParticles;
			numberOfSeconds -= equipment[typeId].increaseRange;
		}

		health += item.increaseHealth;
		numberOfParticles += item.increaseParticles;
		numberOfSeconds += item.increaseRange;

		equipment[typeId] = item;

		playerObject.GetComponentInChildren<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0, numberOfParticles, numberOfParticles, 1, 0));
	}
}
