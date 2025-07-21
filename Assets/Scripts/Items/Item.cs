using UnityEngine;

public abstract class Item:ScriptableObject
{
	public string displayedName,description;
    public bool canStack;
	public int maxStackSize;
}
