using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Scriptable Objects/EntityStats")]
public class EntityStats : ScriptableObject
{
    public int maxHP, maxMana, maxStamina;
}
