using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float time;
	public static float mult = 1;
    void Update()
    {
        time = Time.deltaTime * mult;
    }
	public static void SetMultiplier (float newMult){
		mult = newMult;
	}
}
