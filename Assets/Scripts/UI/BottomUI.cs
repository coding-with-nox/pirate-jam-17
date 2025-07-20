using UnityEngine;

public class BottomUI:Singleton<BottomUI>
{
    bool raised, forced;
	MovingObject mo;
	Vector3 loweredPos, raisedPos;
	[SerializeField]float raiseTime;
	void Start(){
		mo = GetComponent<MovingObject>();
		if (mo == null){
			print ("a "+this+" manca il MovingObject");
		}
		loweredPos = transform.localPosition;
		
		raisedPos = transform.localPosition+(new Vector3(0,(GetComponent<RectTransform>().rect.height)*(3f/4f),0));
	}
	
	public void ToggleRaise(){
		Raise(!raised);
	}
	public void Raise(bool flag, bool force = false){
		if (!forced||force){
			raised = flag;
			forced = force;
			if (raised){
				mo.Move(raisedPos,raiseTime);
			}
			else {
				mo.Move(loweredPos,raiseTime);
			}
		}
	}
	public void ReleaseForced (){
		forced = false;
	}
	public float GetUIHeight(){
		return transform.position.y+(GetComponent<RectTransform>().rect.height/2);
	}
}
