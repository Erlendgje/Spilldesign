using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	public Item item;
	public GameObject textMesh;
	private bool collided;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (collided)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Player.player.AddItem(item);
				Destroy(this.gameObject);
			}
		}
	}

	public void SetActive(bool active)
	{
		textMesh.SetActive(active);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SetActive(true);
			collided = true;
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SetActive(false);
			collided = false;
		}
	}
}
