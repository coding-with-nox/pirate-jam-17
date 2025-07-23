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
	protected Attack attack;
	protected EntityMovement mover;
	public Faction faction;
	float hpRegenTime, manaRegenTime, staminaRegenTime, cannotAttackFor;
	protected bool deathAnimation;
	protected float timeToDie;
	void Start(){
		mover = GetComponent<EntityMovement>();
		Setup(stats);
		
		if (mover == null){
			print (""+this+"Is missing a mover");
		}
	}
	void Update(){
		if (CanAct()){
			RegenResources();
			ExecuteCommands();
		}
	}
	protected virtual void ExecuteCommands(){
		
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
		if (cannotAttackFor>0){
			cannotAttackFor-=TimeManager.time;
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
		
		timeToDie = es.timeToDie;
		mover.SetSpeedValues(es.maxSpeed, es.accelleration);
		attack = es.attack;
	}
	public virtual void Die(DeathType dt){
		mover.Stop();
		deathAnimation = true;
	}
	public virtual Attack GetBaseAttack(){
		return attack;
	}
	public void ReceiveDamage (DamageValue dv){
		if (!deathAnimation){
			print (""+this+": ouchie");
			
			ChangeValue(Resource.health, -dv.val);
			if (dv.slowsPerc >= 0 && dv.slowsFor >0 && dv.slowsPerc < 1f){
				mover.AddSpeedModifier(EntityMovement.SpeedModifier.damage,new Vector2(dv.slowsFor , dv.slowsPerc));
			}
		}
	}
	public void GenerateBaseAttack(Vector3 direction, float distance){
		//print ("attacking");
		GenerateSpecialAttack(GetBaseAttack(), direction, distance);
	}
	public void GenerateSpecialAttack(Attack a,Vector2 direction, float distance){
		//print ("attacking 2");
		if (CanAttack()){
			//print ("can attack");
			DamageArea da = null; //instanziato a null per fare da default
			if (a.attackCost> 0 && !ChangeValueIfPossible(a.attackCostType,-a.attackCost)){
				return;
			}
			//print ("will attack");
			if (a.slowsUserPerc >= 0 && a.slowsUserFor >0 && a.slowsUserPerc < 1f){
				mover.AddSpeedModifier(EntityMovement.SpeedModifier.attacking,new Vector2(a.slowsUserFor, a.slowsUserPerc));
			}
			
			cannotAttackFor = a.coolDown;
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
			if (a.followsUser){
				da.transform.SetParent(transform);
			}
		}
	}
	protected virtual bool CanAttack (){
		return cannotAttackFor<=0 && CanAct();
	}
	public virtual bool CanAct(){
		return !deathAnimation;
	}
}
