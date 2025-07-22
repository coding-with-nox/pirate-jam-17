using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public abstract class Entity : MonoBehaviour
{
    public enum Resource{
		health,
		mana,
		stamina
	}
	public enum Faction{
		neutral,
		player,
		enemy,
		none //NON USARE su entità, serve per gli attacchi indiscriminati (trappole ed esplosioni)
	}
	public enum DeathType{
		intentional,
		damage,
		baseReturn
	}
	Dictionary<Resource, int> values = new();
	Dictionary<Resource, int> maxValues = new();
	[SerializeField]EntityStats stats;
	[SerializeField]protected Attack attack;
	
	public Faction faction;
	float hpRegenTime, manaRegenTime, staminaRegenTime;
	void Start(){
		Setup(stats);
	}
	void Update(){
		RegenResources();
	}
	void RegenResources(){
		if (stats.hpRegenTime > 0){
			hpRegenTime -= TimeManager.time;
			while (hpRegenTime <= 0){
				ChangeValue(Resource.health,1);
				hpRegenTime += stats.hpRegenTime;
			}
		}
		if (stats.manaRegenTime > 0){
			manaRegenTime -= TimeManager.time;
			while (manaRegenTime <= 0){
				ChangeValue(Resource.mana,1);
				manaRegenTime += stats.manaRegenTime;
			}
		}
		if (stats.staminaRegenTime > 0){
			staminaRegenTime -= TimeManager.time;
			while (staminaRegenTime <= 0){
				ChangeValue(Resource.stamina,1);
				staminaRegenTime += stats.staminaRegenTime;
			}
		}
	}
	public void ChangeValue (Resource r, int val){
		values[r] += val;
		if (values[r] < 0){
			values[r] = 0;
		}
		else if (values[r]>maxValues[r]){
			values[r]=maxValues[r];
		}
		CheckStats();
	}
	public bool ChangeValueIfPossible (Resource r, int val){
		if (values[r]+val<maxValues[r] && values[r]+val>=0){
			ChangeValue(r,val);
			return true;
		}
		return false;
	}
	public int GetValue(Resource r, out int max){
		max = maxValues[r];
		return values[r];
	}
	protected virtual void CheckStats(){
		if (values[Resource.health]<= 0){
			Die(DeathType.damage);
		}
	}
	public virtual void Setup (EntityStats es){
		
		maxValues.Add(Resource.health,es.maxHP);
		values.Add(Resource.health,es.maxHP);
		
		maxValues.Add(Resource.mana,es.maxMana);
		values.Add(Resource.mana,es.maxMana);
		
		maxValues.Add(Resource.stamina,es.maxStamina);
		values.Add(Resource.stamina,es.maxStamina);
		
	}
	public virtual void Die(DeathType dt){
		
	}
	public virtual Attack GetBaseAttack(){
		return attack;
	}
	public void ReceiveDamage (DamageValue dv){
		print (""+this+": ouchie");
	}
	public void GenerateBaseAttack(Vector3 direction, float distance){
		GenerateSpecialAttack(GetBaseAttack(), direction, distance);
	}
	public void GenerateSpecialAttack(Attack a,Vector2 direction, float distance){
		DamageArea da = null; //instanziato a null per fare da default
		if (!ChangeValueIfPossible(a.attackCostType,-a.attackCost)){
			return;
		}
		switch (a.shape){
			case (Attack.Shape.square):{
				da = Instantiate(EntityManager.I.squareAttack).GetComponent<DamageArea>();
				break;
			}
			case (Attack.Shape.circle):{
				da = Instantiate(EntityManager.I.circleAttack).GetComponent<DamageArea>();
				break;
			}
		}
		if (da != null){
			float dist = distance;
			if (a.forcedStartingDistance != 0){
				dist = a.forcedStartingDistance;
			}
			else if(a.maxDistance != 0 && dist > a.maxDistance){
				dist = a.maxDistance;
			}
			da.Setup(a, (Vector2)transform.position+(direction*dist), direction, faction);
		}
	}
}
