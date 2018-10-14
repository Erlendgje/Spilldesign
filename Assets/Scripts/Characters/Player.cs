using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	private List<Item> inventory;

	private GameObject playerObject;
    private Controller controller;

	private int health = 1;
    public static bool vision = false;

	public Player()
	{
		inventory = new List<Item>();
	}

	public bool HasItem(Item item)
	{
		return inventory.Contains(item);
	}

	public void AddItem(Item item)
	{
		if(item is Upgrade)
		{
			if (!Player.vision)
			{
				Player.vision = true;
			}
			else
			{
				Upgrade temp = (Upgrade) item;
				ParticleSystem.MainModule newMain = controller.ps.main;
				newMain.startLifetime = newMain.startLifetime.constant + temp.range;
				controller.ps.emission.SetBurst(0, new ParticleSystem.Burst(0, (int) (controller.ps.emission.GetBurst(0).count.constant * ((temp.range / newMain.startLifetime.constant) * 2 + 1))));
				//newEmission.GetBurst(0).count = (int) (newEmission.burstCount * );
			}
		}
		else
		{
			inventory.Add(item);
		}
	}

	public void SetPlayerObject(GameObject player, Controller controller)
	{
		playerObject = player;
		this.controller = controller;
	}

	public Transform GetTransform()
	{
		return playerObject.transform;
	}

	public void RemoveItem(Item item)
	{
		inventory.Remove(item);
	}

	public void canMove(bool canMove)
	{
		controller.setCanMove(canMove);
	}
}
