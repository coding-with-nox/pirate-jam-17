using UnityEngine;
using System;
using System.Collections.Generic;
public class DamageArea : MonoBehaviour
{
    DamageValue damage;
	float totalDuration,duration,timeToDamageTick, damageInterval, speed;
	Vector3 direction,initialSize, finalSize;
	List<Entity> collidingWith = new();
	List<Entity> alreadyHit = new();
	int maxTargets, targetsHit;
	bool moving, expanding, damagesOnEnd;
	static float zPos = 0.001f;
	List<Entity.Faction> ignoreList = new();
	public void Setup (Attack a,Vector2 pos, Vector2 rotationVector, Entity.Faction fac){
		duration = a.duration;
		totalDuration = a.duration;
		damageInterval = a.damageInterval;
		timeToDamageTick = a.damageInterval;
		speed = a.projectileSpeed;
		if (rotationVector.x<0 ){
			transform.rotation = Quaternion.Euler(0,0,-270+(Quaternion.LookRotation(rotationVector,new Vector3(0,0,1f)).eulerAngles.x));
		}
		else {
			transform.rotation = Quaternion.Euler(0,0,270-(Quaternion.LookRotation(rotationVector,new Vector3(0,0,1f)).eulerAngles.x));
		}
		transform.position = new Vector3(pos.x, pos.y,zPos);
		transform.localScale = a.size;
		
		damage = a.damage;
		
		initialSize = a.size;
		expanding = a.expanding;
		finalSize = a.finalSize;
		
		maxTargets = a.maxTargets;
		moving = a.projectile;
		direction = rotationVector;
		damagesOnEnd = a.damagesOnEnd;
		if (!a.friendlyFire){
			
			ignoreList.Add(fac);
		}
	}
	void Update (){
		LowerDuration();
		Move();
	}
	void Move(){
		if (moving){
			transform.position+=(direction*TimeManager.time*speed);
		}
	}
	//in metodo separato per chiarezza nel caso si debba aggiungere animazioni o trail nell'update
	void LowerDuration(){
		duration -= TimeManager.time;
		timeToDamageTick -= TimeManager.time;
		
		if (expanding){
			transform.localScale = (initialSize*(duration/totalDuration))+(finalSize*((totalDuration-duration)/totalDuration));
		}
		if (damageInterval > 0 && timeToDamageTick <= 0){
			timeToDamageTick+= damageInterval;
			DamageColliding();
		}
		
		if (duration<= 0){
			RemoveAttack();
		}
	}
	void DamageColliding(){
		foreach (Entity e in collidingWith){
			//max targets = 0 -> nessun massimo
			if (e != null){//potrebbero essere null se vengono distrutti da un tick precedente (e quindi non escono mai dal collider) 
				if (maxTargets <= 0 || targetsHit < maxTargets){
					e.ReceiveDamage(damage);
					targetsHit++;
				}
				else {
					return;
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collided){
		
        Entity e = collided.GetComponent<Entity>();
		if (e != null && !alreadyHit.Contains(e) && !ignoreList.Contains(e.faction)){
			if (moving){
				e.ReceiveDamage(damage);
				alreadyHit.Add(e);
				targetsHit++;
				if (maxTargets <= 0 || targetsHit >= maxTargets){
					RemoveAttack();
				}
			}
			else if (!collidingWith.Contains(e)) {
				collidingWith.Add(e);
			}
		}
		else if (moving){
			Tile t = collided.GetComponent<Tile>();
			if (t!= null && t.isWall){
				RemoveAttack();
			}
		}
    }
	void OnTriggerExit2D(Collider2D collided){
		
        Entity e = collided.GetComponent<Entity>();
		if (e != null && collidingWith.Contains(e)){
			collidingWith.Remove(e);
		}
    }
	public void RemoveAttack(){
		//metodo a parte nel caso ci si voglia mettere qualcosa tipo esplosioni / vfx che vengono generate alla distruzione del proiettile
		
		if (damagesOnEnd){
			DamageColliding();
		}
		
		Destroy(gameObject);
	}
}
