using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "Items/Item", order = 1)]
[System.Serializable]
public class Item : ScriptableObject {

	public string itemName;
}
