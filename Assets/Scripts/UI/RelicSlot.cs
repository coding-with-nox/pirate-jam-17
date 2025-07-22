using UnityEngine;

public class RelicSlot: InventorySlot
{

    public override bool MoveItem(InventoryItem i){
		if (i.attached.GetType() == typeof(Relic)){
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
