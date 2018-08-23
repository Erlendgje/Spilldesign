using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "Dialogue/Directions", order = 1)]
[System.Serializable]
public class Directions : ScriptableObject {
	[TextArea]
	public string rightDirection;
	[TextArea]
	public string wrongDirection;
	[TextArea]
	public string direction;
	[TextArea]
	public string arrived;
}
