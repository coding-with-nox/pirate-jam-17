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
		damage
	}
	[SerializeField]float maxSpeed, accelleration;
	[SerializeField]protected float dashTime, dashCooldown, dashSpeedMultiplier;
	protected float dashTimeRemaining, dashCooldownRemaining;
	Rigidbody2D rb;
	bool setUp;
    protected Dictionary<SpeedModifier,float>speedMultipliers = new();
	
    void Start()
    {
        Setup();
    }

    
    void Update()
    {
		Move();
		
        LimitSpeed();
    }
	
	float GetFinalMaxSpeed (){
		float result = maxSpeed;
		foreach (KeyValuePair<SpeedModifier,float> kvp in speedMultipliers){
			maxSpeed *= kvp.Value;
		}
		return result;
	}
	//DA FINIRE WIP
	protected void AddSlowdown(){
		
	}
	protected void RemoveSlowdown(){
		
	}
	public virtual void Setup (){
		if (setUp){
			return;
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
	protected void Accellerate(Vector3 v){
		rb.linearVelocity+= (Vector2)(v*accelleration);
	}
	protected void Accellerate(Vector2 v){
		rb.linearVelocity+= (v*accelleration);
	}
	protected void MultiplySpeed(float val){
		rb.linearVelocity*= val;
	}
	protected void AccellerateToMax(){
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
		rb.linearVelocity = dir.normalized*rb.linearVelocity.magnitude;
	}
}
