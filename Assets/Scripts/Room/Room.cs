using UnityEngine;
using System;
using System.Collections.Generic;
public class Room : MonoBehaviour
{
    EnemySpawn [] enemyList;
	EnemySpawn [] enemyListDead;
	TileSet usedTileSet;
	int sizeX,sizeY;
	static float currentHeight;
	public Room EnterRoom (RoomStats rs, TileSet ts){
		GenerateTiles(rs,ts);
		RepositionPlayer ();
		SpawnEnemies ();
		return this;
	}
	public void RepositionPlayer (){
		Player.I.transform.SetParent(transform);
		Player.I.transform.localPosition = new Vector3(0.5f*sizeX, 1f, 0);

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
			
			e.transform.localPosition = new Vector3(es.spawnPerc.x*sizeX, es.spawnPerc.y*sizeY, 0);
			if (e.transform.localPosition.x >= (float)sizeX-1){
				e.transform.localPosition = new Vector3((float)sizeX-1,e.transform.localPosition.y,0);
			}
			if (e.transform.localPosition.y >= (float)sizeY-1){
				e.transform.localPosition = new Vector3(e.transform.localPosition.x,(float)sizeX-1,0);
			}
			e.Setup(es.enemy);
		}
		else {
			print ("Error, couldn't spawn enemy");
		}
	}
	void SpawnChests(RoomStats rs){
		foreach (Vector2 chestPos in rs.chestList){
			Instantiate(RoomManager.I.chestPrefab,Vector3.zero,Quaternion.identity, transform).GetComponent<Chest>().Setup(GetRandomChestSprite(),(float)sizeX*chestPos.x,(float)sizeY*chestPos.y);
		}
	}
	public void GenerateTiles(RoomStats rs, TileSet ts){
		sizeX = UnityEngine.Random.Range(rs.sizeXMin,rs.sizeXMax);
		sizeY = UnityEngine.Random.Range(rs.sizeYMin,rs.sizeYMax);
		transform.position = new Vector3(0,(float)currentHeight,0.1f*RoomManager.currentRoom);
		enemyListDead = rs.enemyListDead;
		enemyList = rs.enemyList;
		currentHeight +=sizeY;
		usedTileSet = ts;
		for (int x = 0; x<= sizeX; x++){
			for (int y = 0; y<= sizeY; y++){
				if (x == 0 || y == 0 || x == sizeX ||y == sizeY){
					Instantiate(RoomManager.I.tilePrefab,Vector3.zero,Quaternion.identity, transform).GetComponent<Tile>().Setup(GetRandomWallSprite(),(float)x,(float)y);
				}
				else {
					Instantiate(RoomManager.I.tilePrefab,Vector3.zero,Quaternion.identity, transform).GetComponent<Tile>().Setup(GetRandomTileSprite(),(float)x,(float)y , y == 1,x == 1  , y == sizeY-1, x == sizeX-1);
				}
			}
		}
		SpawnChests(rs);
	}
	Sprite GetRandomWallSprite(){
		if (usedTileSet.wallList.Length>0){
			return usedTileSet.wallList[UnityEngine.Random.Range(0,usedTileSet.wallList.Length)];
		}
		return null;
	}
	Sprite GetRandomTileSprite(){
		if (usedTileSet.tileList.Length>0){
			return usedTileSet.tileList[UnityEngine.Random.Range(0,usedTileSet.tileList.Length)];
		}
		return null;
	}
	Sprite GetRandomChestSprite(){
		if (usedTileSet.chestList.Length>0){
			return usedTileSet.chestList[UnityEngine.Random.Range(0,usedTileSet.chestList.Length)];
		}
		return null;
	}
	Sprite GetRandomDoorSprite(){
		if (usedTileSet.doorList.Length>0){
			return usedTileSet.doorList[UnityEngine.Random.Range(0,usedTileSet.doorList.Length)];
		}
		return null;
	}
	Sprite GetRandomClutterSprite(){
		if (usedTileSet.clutterList.Length>0){
			return usedTileSet.clutterList[UnityEngine.Random.Range(0,usedTileSet.clutterList.Length)];
		}
		return null;
	}
}
