using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : Item
{
    public Attack attack;
	public string displayName, description;
}
