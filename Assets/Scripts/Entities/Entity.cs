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
	[SerializeField]EntityStats stats;
	float hpRegenTimeTo, manaRegenTimeTo, staminaRegenTimeTo;
	void Start (){
		if (stats != null){
			Setup(stats);
		}
		else {
			print ("Stats mancanti per "+this);
		}
	}
	void Update (){
		RegenResources();
	}
	void RegenResources(){
		if (stats.hpRegenTime > 0){
			hpRegenTimeTo -= TimeManager.time;
			while (hpRegenTimeTo <= 0){
				ChangeValue(Resource.health,1);
				hpRegenTimeTo += stats.hpRegenTime;
			}
		}
		if (stats.manaRegenTime > 0){
			manaRegenTimeTo -= TimeManager.time;
			while (manaRegenTimeTo <= 0){
				ChangeValue(Resource.mana,1);
				manaRegenTimeTo += stats.manaRegenTime;
			}
		}
		if (stats.staminaRegenTime > 0){
			staminaRegenTimeTo -= TimeManager.time;
			while (staminaRegenTimeTo <= 0){
				ChangeValue(Resource.stamina,1);
				staminaRegenTimeTo += stats.staminaRegenTime;
			}
		}
	}
	public void ChangeValue (Resource r, int val){
		values[r] += val;
		if (values[r] > maxValues[r]){
			values[r] = maxValues[r];
		}
		OnValueChanged();
	}
	protected virtual void OnValueChanged(){
		if (values[Resource.health]<= 0){
			Die();
		}
	}
	public bool ChangeValueIfPossible (Resource r, int val){
		if (values[r] + val >= 0 && values[r] + val <= maxValues[r]){
			ChangeValue(r,val);
			return true;
		}
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
	public int GetValue (Resource r, out int max){
		max = maxValues[r];
		return values[r];
	}
	public void Die(){
		
	}
}
