using UnityEngine;
using System;
public class Room : MonoBehaviour
{
    public EnemySpawn [] enemyList;
	public EnemySpawn [] enemyListDead;
	[SerializeField]Vector3 spawnPoint;
	public void EnterRoom (){
		Player.I.transform.SetParent(transform);
		Player.I.transform.localPosition = spawnPoint;
		SpawnEnemies ();
	}
	public void SpawnEnemies (){
		EnemySpawn [] list;
		if (Player.playerAlive){
			list = enemyList;
		}
		else {
			list = enemyListDead;
		}
		foreach (EnemySpawn es in list){
			SpawnEnemy(es);
		}
	}
	public void SpawnEnemy (EnemySpawn es){
		Enemy e = Instantiate(RoomManager.I.enemyPrefab,Vector3.zero,Quaternion.identity, transform).GetComponent<Enemy>();
		if (e != null){
			e.transform.position = es.spawnPoint;
			e.Setup(es.enemy);
		}
	}
}
