using UnityEngine;

public class InteractRadius : MonoBehaviour
{
	[SerializeField]Player player;
	void OnTriggerEnter2D(Collider2D collided){
		//print ("triggered");
        Interactable i = collided.GetComponent<Interactable>();
		if (i != null){
			player.EnterInteract(i);
		}
		else {
			//print ("discarded");
		}
    }
	void OnTriggerExit2D(Collider2D collided){
		//print ("triggered exit");
        Interactable i = collided.GetComponent<Interactable>();
		if (i != null){
			player.ExitInteract(i);
		}
    }
}
