using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "Dialogue/Conversation", order = 1)]
[System.Serializable]
public class Dialogue : ScriptableObject
{
	public string npcName;
	public conversations[] conversations;
	public bool disappear;
}

[System.Serializable]
public class conversations
{
	public Line[] messages;
	public bool dissapear;
	public float time;
	public bool accessDoors;
}

[System.Serializable]
public class Line
{
	[TextArea]
	public string line;
	public bool guiding;
	public bool pressE;
	public Item item;
}
