using UnityEngine;

public class Chest : Interactable
{
	Item itemA, itemB;
	bool interacted;
	public int forcedRarity = -2;
	void SpawnItems(){
		if (forcedRarity == -2){
			itemA = RoomManager.I.GetRandomItem();
			itemB = RoomManager.I.GetRandomItem();
		}
		else {
			itemA = RoomManager.I.GetRandomItem(forcedRarity);
			itemB = RoomManager.I.GetRandomItem(forcedRarity);
		}
	}
    public override void Interact(){
		if (!interacted){
			interacted = true;
			SpawnItems();
			ContainerInventory.I.Setup(itemA,itemB);
			ContainerInventory.I.Show();
			BottomUI.I.Raise(true,true);
			PlayerMovement.I.OpenChest();
		}
	}
	public override bool CanInteract(){
		return !interacted;
	}
	public override void Highlight(bool flag){
		
	}
}
