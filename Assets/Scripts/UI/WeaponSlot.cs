using UnityEngine;

public class WeaponSlot : InventorySlot
{
    public override bool MoveItem(InventoryItem i){
		if (i.attached.GetType() == typeof(Weapon)){
			if (attached != null){
				attached.Rebase(i.slot);
			}
			else {
				i.slot.Empty();
			}
			i.Rebase(this);
			CheckStats();
			return true;
		}
		return false;
	}
	public override void Empty(){
		base.Empty();
		CheckStats();
	}
	public override void Clear(){
		base.Clear();
		CheckStats();
	}
	public void CheckStats(){
		
	}
}
