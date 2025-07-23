using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Scriptable Objects/EntityStats")]
public class EntityStats : ScriptableObject
{
    public int maxHP, maxMana, maxStamina;
	public float hpRegenTime, manaRegenTime, staminaRegenTime, maxSpeed, accelleration, timeToDie, attackRange;
	public Attack attack;
}
