using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class Player : Entity
{
    List<Interactable> currentlyInteracting = new();
	bool interacting; //interacting serve per quando i contenitori sono aperti/altro
	int interactingWith = -1; // -1 equivale a dire false
	[SerializeField]Slider healthSlider,manaSlider,staminaSlider;
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
	protected override void OnValueChanged(){
		base.OnValueChanged();
		SetSliders();
	}
	void SetSliders(){
		int val, max;
		val = GetValue(Resource.health, out max);
		healthSlider.value = (float)val/(float)max;
		
		val = GetValue(Resource.mana, out max);
		manaSlider.value = (float)val/(float)max;
		
		val = GetValue(Resource.stamina, out max);
		staminaSlider.value = (float)val/(float)max;
	}
	
}
