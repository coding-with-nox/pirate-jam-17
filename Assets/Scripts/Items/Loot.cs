using UnityEngine;
[CreateAssetMenu(fileName = "Loot", menuName = "Scriptable Objects/Loot")]
public class Loot:Item
{
    public enum Resource{
		stone,
		wood,
		metal,
		runes,
		gems
	}
	public Resource type;
}
