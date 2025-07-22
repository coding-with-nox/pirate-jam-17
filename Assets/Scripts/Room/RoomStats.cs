using UnityEngine;

[CreateAssetMenu(fileName = "RoomStats", menuName = "Scriptable Objects/RoomStats")]
public class RoomStats : ScriptableObject
{
    public EnemySpawn [] enemyList;
	public EnemySpawn [] enemyListDead;
	public Vector2[] chestList;
	public int sizeXMax, sizeYMax, sizeXMin, sizeYMin;
	public float doorPosition;
}
