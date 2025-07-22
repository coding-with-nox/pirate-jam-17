using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class Door : Interactable
{
	static List<Door> doorList;
	bool interacted;
	void Start(){
		doorList.Add(this);
	}
	public override void Interact(){
		if (!interacted){
			interacted = true;
			
		}
	}
	public override bool CanInteract(){
		return !interacted;
	}
	public override void Highlight(bool flag){
		
	}
	public static void ResetDoors(){
		foreach (Door d in doorList){
			d.interacted = false;
		}
	}
}
