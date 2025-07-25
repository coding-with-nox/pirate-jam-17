using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();
	public abstract void Highlight(bool flag);
	public abstract bool CanInteract();
}
