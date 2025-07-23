using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public abstract class EntityMovement : MonoBehaviour
{
	public enum SpeedModifier{
		interacting,
		armor,
		cold,
		terrain,
		trap,
		damage,
		attacking
	}
	float maxSpeed, accelleration;
	[SerializeField]protected float dashTime, dashCooldown, dashSpeedMultiplier;
	protected float dashTimeRemaining, dashCooldownRemaining;
	Entity entity;
	Rigidbody2D rb;
	bool setUp;
    protected Dictionary<SpeedModifier,Vector2>speedMultipliers = new();
    void Start()
    {
        Setup();
    }

    
    void Update()
    {
		TickDownSpeedModifiers(TimeManager.time);
		Move();
		
        LimitSpeed();
    }
	public void SetSpeedValues(float newSpeed,float newAcc){
		maxSpeed = newSpeed;
		accelleration = newAcc;
	}
	float GetFinalMaxSpeed (){
		float result = maxSpeed;
		foreach (KeyValuePair<SpeedModifier,Vector2> kvp in speedMultipliers){
			//print ("found "+kvp.Key+": "+kvp.Value);
			result *= kvp.Value.y;
		}
		return result;
	}
	void TickDownSpeedModifiers(float time){
		Dictionary<SpeedModifier,Vector2> newValue = new();
		foreach (KeyValuePair<SpeedModifier,Vector2> kvp in speedMultipliers){
			if (kvp.Value.x-time > 0 || kvp.Value.x == 0){
				newValue.Add(kvp.Key,new Vector2(kvp.Value.x-time,kvp.Value.y));
			}
		}
		speedMultipliers = newValue;
		
	}
	public void AddSpeedModifier(SpeedModifier sm, Vector2 val){
		if (speedMultipliers.ContainsKey(sm)){
			speedMultipliers[sm] = val;
		}
		else {
			speedMultipliers.Add(sm,val);
		}
	}
	public void RemoveSpeedModifier(SpeedModifier sm){
		if (speedMultipliers.ContainsKey(sm)){
			speedMultipliers.Remove(sm);
		}
	}
	public void ClearSpeedModifiers(){
		speedMultipliers.Clear();
	}
	public virtual void Setup (){
		if (setUp){
			return;
		}
		entity = GetComponent<Entity>();
		if (entity == null){
			print ("Error while getting the entity for "+this);
		}
		rb = GetComponent<Rigidbody2D>();
		if (rb == null){
			print ("Error while getting the rb for "+this);
		}
		setUp = true;
	}
	void LimitSpeed(){
		if (rb.linearVelocity.magnitude >= GetFinalMaxSpeed () && dashTimeRemaining <= 0){
			rb.linearVelocity = rb.linearVelocity.normalized*GetFinalMaxSpeed ();
		}
	}
	protected virtual void Move(){
		
	}
	public void Stop(){
		rb.linearVelocity = Vector3.zero;
	}
	public void SlowDown(){
		if (!entity.CanAct()){
			return;
		}
		Accellerate(-rb.linearVelocity.normalized);
	}
	public void Accellerate(Vector3 v){
		if (!entity.CanAct()){
			return;
		}
		rb.linearVelocity+= (Vector2)(v*accelleration);
	}
	public void Accellerate(Vector2 v){
		if (!entity.CanAct()){
			return;
		}
		rb.linearVelocity+= (v*accelleration);
	}
	public void MultiplySpeed(float val){
		if (!entity.CanAct()){
			return;
		}
		rb.linearVelocity*= val;
	}
	public void AccellerateToMax(){
		if (!entity.CanAct()){
			return;
		}
		rb.linearVelocity = rb.linearVelocity.normalized*GetFinalMaxSpeed ();
	}
	public float GetSpeedMagnitude(){
		return rb.linearVelocity.magnitude;
	}
	void OnCollisionEnter2D(Collision2D collided){
		if (dashTimeRemaining > 0){
			dashTimeRemaining = 0;
			MultiplySpeed(0);
		}
    }
	public void RotateSpeed(Vector2 dir){
		if (!entity.CanAct()){
			return;
		}
		rb.linearVelocity = dir.normalized*rb.linearVelocity.magnitude;
	}
}
