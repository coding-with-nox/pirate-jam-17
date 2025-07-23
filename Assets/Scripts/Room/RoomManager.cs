using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class RoomManager: Singleton<RoomManager>
{
	
	
	void Start(){
		SpawnStartingRoom();
	}
	
	
    List<Room>currentRoomList = new();
	public static int currentRoom;
	public TileSet [] tileSetList;
	public Item [] matList, itemListR1,itemListR2,itemListR3,itemListR4,itemListR5,itemListRB;
	public RoomStats[] roomList, bossRoomList;
	[SerializeField]RoomStats startingRoomStats;
	public GameObject roomPrefab,enemyPrefab, chestPrefab, doorPrefab, tilePrefab;
	public void EnterNewRoom(){
		currentRoom++;
		currentRoomList.Add(Instantiate(roomPrefab).GetComponent<Room>().EnterRoom(GetRandomRoomStats(currentRoom == 10),GetRandomTileSet()));
		
	}
	void SpawnStartingRoom(){
		Instantiate(roomPrefab).GetComponent<Room>().EnterRoom(startingRoomStats,tileSetList[0]);
	}
	public RoomStats GetRandomRoomStats(bool isBoss){
		if (isBoss){
			if (bossRoomList.Length>0){
				return bossRoomList[UnityEngine.Random.Range(0,bossRoomList.Length)];
			}
			return null;
		}
		if (roomList.Length>0){
			return roomList[UnityEngine.Random.Range(0,roomList.Length)];
		}
		return null;
	}
	public Item GetRandomItem(){
		if (UnityEngine.Random.Range(0,2) == 1){  //per i random tra 2 integri unity non risponde mai con il massimo, quindi questo intervallo rende solo 0 o 1
			switch (currentRoom/2){
				case 0:{
					if (itemListR1.Length>0){
						return itemListR1[UnityEngine.Random.Range(0,itemListR1.Length)];
					}
					break;
				}
				case 1:{
					if (itemListR2.Length>0){
						return itemListR2[UnityEngine.Random.Range(0,itemListR2.Length)];
					}
					break;
				}
				case 2:{
					if (itemListR3.Length>0){
						return itemListR3[UnityEngine.Random.Range(0,itemListR3.Length)];
					}
					break;
				}
				case 3:{
					if (itemListR4.Length>0){
						return itemListR4[UnityEngine.Random.Range(0,itemListR4.Length)];
					}
					break;
				}
				case 4:{
					if (itemListR5.Length>0){
						return itemListR5[UnityEngine.Random.Range(0,itemListR5.Length)];
					}
					break;
				}
			}
		}
		return matList[UnityEngine.Random.Range(0,matList.Length)]; 
		//fà anche da default per lo switch
	}
	public Item GetRandomItem(int specialRarity){
		switch (specialRarity){
			case 0:{
				if (itemListR1.Length>0){
					return itemListR1[UnityEngine.Random.Range(0,itemListR1.Length)];
				}
				break;
			}
			case 1:{
				if (itemListR2.Length>0){
					return itemListR2[UnityEngine.Random.Range(0,itemListR2.Length)];
				}
				break;
			}
			case 2:{
				if (itemListR3.Length>0){
					return itemListR3[UnityEngine.Random.Range(0,itemListR3.Length)];
				}
				
				break;
			}
			case 3:{
				if (itemListR4.Length>0){
					return itemListR4[UnityEngine.Random.Range(0,itemListR4.Length)];
				}
				break;
			}
			case 4:{
				if (itemListR5.Length>0){
					return itemListR5[UnityEngine.Random.Range(0,itemListR5.Length)];
				}
				break;
			}
			case 5:{
				if (itemListRB.Length>0){
					return itemListRB[UnityEngine.Random.Range(0,itemListRB.Length)];
				}
				break;
			}
		}
		return matList[UnityEngine.Random.Range(0,matList.Length)]; 
	}
	public TileSet GetRandomTileSet(){
		if (tileSetList != null && tileSetList.Length >0){
			return tileSetList[UnityEngine.Random.Range(0,tileSetList.Length)]; 
		}
		print ("Missing TileSets");
		return null;
		
	}
}
