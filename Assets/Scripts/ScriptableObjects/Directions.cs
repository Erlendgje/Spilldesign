using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "Dialogue/Directions", order = 1)]
[System.Serializable]
public class Directions : ScriptableObject {

	public string rightDirection;
	public string wrongDirection;
	public string direction;
	public string arrived;
}
