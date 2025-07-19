using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    Vector3 initialPos,endPos;
	protected float timeToMove; 
	float timeMoved;
	Vector3 initialRot,endRot;
	float timeToRot, timeRot;
	bool moving, rotating;
	bool frozen;
	MovedObject listener,rotListener;
	void Update(){
		if (moving && !frozen){
			timeMoved += Time.deltaTime;
			float perMov = 1f;
			if (timeToMove != 0){
				perMov = timeMoved/timeToMove;
			}
			if (timeMoved >= timeToMove){
				moving = false;
				perMov = 1f;
				
			}
			transform.localPosition = (initialPos*(1f-perMov))+(endPos*perMov);
			if (listener != null && perMov >= 1f){
				listener.ReachedTile();
			}
		}
		if (rotating && !frozen){
			timeRot += Time.deltaTime;
			float perRot = 1f;
			if (timeToMove != 0){
				perRot = timeRot/timeToRot;
			}
			if (timeRot >= timeToRot){
				rotating = false;
				perRot = 1f;
			}
			
			transform.localRotation = Quaternion.Euler(initialRot*(1f-perRot)+(endRot*perRot));
			if (listener != null && perRot >= 1f){
				listener.ReachedRotation();
			}
		}
	}
	public void Move(Vector3 newPos, float time = 0, MovedObject responseListener = null){
		timeMoved = 0;
		initialPos = transform.localPosition;
		listener = responseListener;
		endPos = newPos;
		timeToMove = time;
		if (timeToMove> 0){
			moving = true;
		}
		else {
			transform.localPosition = (newPos);
		}
	}
	public void Rotate(Vector3 newRot, float time, MovedObject responseListener){
		Rotate (newRot, time);
		rotListener = responseListener;
	}
	public void Rotate(Vector3 newRot, float time){
		rotListener = null;
		initialRot = transform.rotation.eulerAngles;
		if (newRot.x <=90f && initialRot.x >= 270f){
			initialRot.x -= 360f;
		}
		if (newRot.y <=90f && initialRot.y >= 270f){
			initialRot.y -= 360f;
		}
		if (newRot.z <=90f && initialRot.z >= 270f){
			initialRot.z -= 360f;
		}
		if (newRot.x >=270f && initialRot.x <= 90f){
			initialRot.x += 360f;
		}
		if (newRot.y >=270f && initialRot.y <= 90f){
			initialRot.y += 360f;
		}
		if (newRot.z >=270f && initialRot.z <= 90f){
			initialRot.z += 360f;
		}
		
		endRot = newRot;
		timeRot = 0;
		timeToRot = time;
		if (timeToRot>0){
			rotating = true;
		}
		else {
			transform.localRotation = Quaternion.Euler(newRot);
		}
	}
	public void Freeze (bool flag){
		frozen = flag;
	}
	public bool IsActing(){
		return (moving || rotating);
	}
}
public interface MovedObject {
	public void ReachedTile();
	public void ReachedRotation();
}
