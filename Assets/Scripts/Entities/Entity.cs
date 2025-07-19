using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class Entity : MonoBehaviour
{
    public enum Resource{
		health,
		mana,
		stamina
	}
	Dictionary<Resource, int> values = new();
	Dictionary<Resource, int> maxValues = new();
	public void ChangeValue (Resource r, int val){
		
	}
	public bool ChangeValueIfPossible (){
		return false;
	}
	public void Setup (EntityStats es){
		
		maxValues.Add(Resource.health,es.maxHP);
		values.Add(Resource.health,es.maxHP);
		
		maxValues.Add(Resource.mana,es.maxMana);
		values.Add(Resource.mana,es.maxMana);
		
		maxValues.Add(Resource.stamina,es.maxStamina);
		values.Add(Resource.stamina,es.maxStamina);
		
	}
}
