using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

	private List<Vector4> collisions;
	private List<Color> colorArray;
	private static int MAX_COLLISIONS = 264;
	private static float FADE = 0.5f;
	private Material myMaterial;
	private List<ParticleCollisionEvent> collisionEvents;

	private void Start()
	{
		collisions = new List<Vector4>(MAX_COLLISIONS);
        colorArray = new List<Color>(MAX_COLLISIONS);
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
                colorArray.RemoveAt(0);
			}
			Vector4 position = collisionEvents[i].intersection;
			collisions.Add(position);
            colorArray.Add(other.GetComponentInParent<Character>().color);
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
			float fade = colorArray[i].a - FADE * Time.deltaTime;
            colorArray[i] = new Vector4(colorArray[i].r, colorArray[i].g, colorArray[i].b, fade);
			if (colorArray[i].a <= 0)
			{
				collisions.RemoveAt(i);
                colorArray.RemoveAt(i);
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
		Color[] mySecondArray = new Color[MAX_COLLISIONS];
		colorArray.ToArray().CopyTo(mySecondArray, 0);

		this.GetComponent<MeshRenderer>().material.SetVectorArray("_Collisions", myArray);
		this.GetComponent<MeshRenderer>().material.SetColorArray("_CollisionColor", mySecondArray);
		this.GetComponent<MeshRenderer>().material.SetFloat("_ArrayLength", (float)collisions.Count);
	}
}
