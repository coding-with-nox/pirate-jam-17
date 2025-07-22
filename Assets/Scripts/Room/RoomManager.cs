using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class RoomManager: Singleton<RoomManager>
{
	
    List<Room>roomList;
	static int currentRoom;
	
	public Item [] matList, itemListR1,itemListR2,itemListR3,itemListR4,itemListR5,itemListRB;
	public GameObject enemyPrefab;
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
}
