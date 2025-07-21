using UnityEngine;

public class ContainerInventory : Singleton<ContainerInventory>
{
	[SerializeField]InventorySlot slotA, slotB;
	bool showing;
    void Start(){
		// deve essere attivo per fare lo Start e l'Awake, che serve a sua volta per il Singleton
		Hide();
	}
	public void Hide(){
		SetActive(false);
	}
	public void Show(){
		SetActive(true);
	}
	public void Toggle(){
		SetActive(!showing);
	}
	void SetActive(bool flag){
		showing = flag;
		gameObject.SetActive(flag);
	}
	public void Setup (Item a, Item b){
		slotA.SpawnItem(a);
		slotB.SpawnItem(b);
	}
}
