using UnityEngine;

public class EntityMovement : MonoBehaviour
{
	[SerializeField]float maxSpeed, accelleration;
	[SerializeField]protected float dashTime, dashCooldown, dashSpeedMultiplier;
	protected float dashTimeRemaining, dashCooldownRemaining;
	Rigidbody2D rb;
	bool setUp;
    
    void Start()
    {
        Setup();
    }

    
    void Update()
    {
		Move();
		
        LimitSpeed();
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
		if (rb.linearVelocity.magnitude >= maxSpeed && dashTimeRemaining <= 0){
			rb.linearVelocity = rb.linearVelocity.normalized*maxSpeed;
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
		rb.linearVelocity = rb.linearVelocity.normalized*maxSpeed;
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
