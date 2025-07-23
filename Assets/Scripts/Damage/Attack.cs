using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack")]
public class Attack : ScriptableObject
{
	public enum Shape {
		square,
		circle
	}
	public Shape shape;
	public Vector3 size, finalSize;
    public DamageValue damage;
	public float duration, damageInterval, projectileSpeed, forcedStartingDistance, maxDistance, slowsUserFor, slowsUserPerc, coolDown;
	public int maxTargets;
	public bool friendlyFire, projectile, expanding, damagesOnEnd, followsUser;
	public int attackCost;
	public Entity.Resource attackCostType;
}
