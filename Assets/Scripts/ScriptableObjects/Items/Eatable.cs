using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Eatable")]
public class Eatable : Item {

	public float increaseRange;
	public short increaseParticles;
	public int healFor;

	[Tooltip("0 = Head, 1 = Torso, 2 = Legs, 3 = Feets")]
	public int type;
	public int increaseHealth;
}
