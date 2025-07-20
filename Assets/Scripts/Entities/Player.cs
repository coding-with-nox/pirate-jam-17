using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class Player : Entity
{
    List<Interactable> currentlyInteracting = new();
	bool interacting; //interacting serve per quando i contenitori sono aperti/altro
	int interactingWith = -1; // -1 equivale a dire false
	[SerializeField] Slider hpSlider,manaSlider,staminaSlider;
	Weapon equippedWeapon;
	public void EnterInteract (Interactable i){
		currentlyInteracting.Add(i);
		if (!interacting){
			if (interactingWith >=0){
				currentlyInteracting[interactingWith].Highlight(false);
			}
			interactingWith = currentlyInteracting.Count-1;
			currentlyInteracting[interactingWith].Highlight(true);
		}
	}
	public void ExitInteract (Interactable i){
		if (currentlyInteracting.Contains(i)){
			if (interactingWith == currentlyInteracting.IndexOf(i)){
				
				currentlyInteracting[interactingWith].Highlight(false);
				interactingWith--;
				if (interactingWith >=0){
					currentlyInteracting[interactingWith].Highlight(true);
				}
				else if(currentlyInteracting.Count>1){
					interactingWith = 0;
					currentlyInteracting[interactingWith].Highlight(true);
				}
			}
			
			currentlyInteracting.Remove(i);
		}
	}
	public void Interact(){
		if (interactingWith>= 0){
			currentlyInteracting[interactingWith].Interact();
		}
	}
	protected override void CheckStats (){
		base.CheckStats();
		SetSliders();
	}
	void SetSliders(){
		int val, max;
		val = GetValue(Entity.Resource.health,out max);
		hpSlider.value = (float)val/(float)max;
		
		val = GetValue(Entity.Resource.stamina,out max);
		staminaSlider.value = (float)val/(float)max;
		
		val = GetValue(Entity.Resource.mana,out max);
		manaSlider.value = (float)val/(float)max;
	}
	public override void Setup (EntityStats es){
		base.Setup(es);
		faction = Entity.Faction.player;
	}
	public override Attack GetBaseAttack(){
		if (equippedWeapon != null){
			return equippedWeapon.attack;
		}
		return attack;
	}
}
