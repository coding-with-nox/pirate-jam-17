using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class Enemy : Entity
{
	[SerializeField] float attackRange;
	static List<Enemy> enemyList = new();
    public override void Setup (EntityStats es){
		base.Setup(es);
		faction = Entity.Faction.enemy;
		enemyList.Add(this);
		attackRange = es.attackRange;
	}
	protected override void ExecuteCommands(){
		Vector3 distanceToPlayerVector = Player.I.transform.position-transform.position;
		float distanceToPlayer = distanceToPlayerVector.magnitude;
		if (distanceToPlayer > attackRange){
			mover.Accellerate(distanceToPlayerVector.normalized);
		}
		else{
			mover.SlowDown();
			//print ("I vill attack you");
			GenerateBaseAttack (distanceToPlayerVector.normalized, distanceToPlayer);
			//i nemici simulano un click direttamente sul giocatore
		}
	}
	public override void Die(Entity.DeathType dt){
		base.Die(dt);
		enemyList.Remove(this);
		Destroy(gameObject,timeToDie);
	}
	public static bool AnyAlive(){
		return enemyList.Count>0;
	}
}
