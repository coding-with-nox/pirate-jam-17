using UnityEngine;

public class Inventory : Singleton<Inventory>
{
	[SerializeField]WeaponSlot weapon;
	[SerializeField]RelicSlot relic;
    [SerializeField]InventorySlot[] slots;
	public GameObject itemTemplate;
	public bool DropItem (InventoryItem i, Vector2 pos){
		if (weapon.IsInside(pos)){
			return weapon.MoveItem(i);
		}
		if (relic.IsInside(pos)){
			return relic.MoveItem(i);
		}
		foreach (InventorySlot ins in slots){
			if (ins.IsInside(pos)){
				return ins.MoveItem(i);
			}
		}
		return false;
	}
	/*
	public bool DropWeapon (Weapon i, Vector2 pos){
		
		if (weapon.IsInside(pos)){
			return weapon.MoveWeapon(i);
		}
		foreach (InventorySlot ins in slots){
			if (ins.IsInside(pos)){
				return ins.MoveItem(i);
			}
		}
		return false;
	}
	public bool DropRelic (Relic i, Vector2 pos){
		
		if (relic.IsInside(pos)){
			return relic.MoveRelic(i);
		}
		foreach (InventorySlot ins in slots){
			if (ins.IsInside(pos)){
				return ins.MoveItem(i);
			}
		}
		return false;
	}
	*/
}
