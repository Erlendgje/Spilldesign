using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	private List<Vector4> collisions;
	private static int MAX_COLLISIONS = 264;
	private static float FADE = 0.5f;
	private Material myMaterial;
	private List<ParticleCollisionEvent> collisionEvents;

	private void Start()
	{
		collisions = new List<Vector4>(MAX_COLLISIONS);
		myMaterial = new Material(Shader.Find("Unlit/WorldShader"));
		this.GetComponent<MeshRenderer>().material = myMaterial;
		collisionEvents = new List<ParticleCollisionEvent>();
	}

	private void OnParticleCollision(GameObject other)
	{

		int numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);

		for(int i = 0; i < numCollisionEvents; i++) {
			if (collisions.Count >= MAX_COLLISIONS)
			{
				collisions.RemoveAt(0);
			}
			Vector4 position = collisionEvents[i].intersection;
			position.w = 1;
			collisions.Add(position);
		}
	}


	private void Update()
	{
		if(collisions.Count > 0)
		{
			UpdateShader();
		}

		for(int i = 0; i < collisions.Count; i++)
		{
			float fade = collisions[i].w - FADE * Time.deltaTime;
			collisions[i] = new Vector4(collisions[i].x, collisions[i].y, collisions[i].z, fade);
			if (collisions[i].w <= 0)
			{
				collisions.RemoveAt(i);
				if(collisions.Count == 0)
				{
					UpdateShader();
				}
				i--;
			}
			
		}
	}

	private void UpdateShader()
	{
		Vector4[] myArray = new Vector4[MAX_COLLISIONS];
		collisions.ToArray().CopyTo(myArray, 0);
		this.GetComponent<MeshRenderer>().material.SetVectorArray("_Collisions", myArray);
		this.GetComponent<MeshRenderer>().material.SetFloat("_ArrayLength", (float)collisions.Count);
	}
}
