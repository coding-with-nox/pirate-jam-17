using UnityEngine;
using UnityEngine.EventSystems;
using System;
[Serializable]
public class InventorySlot : MonoBehaviour
{
	public InventoryItem attached;
	[SerializeField] bool isInventory;
    public bool IsInside(Vector2 pos){
		//la funzione rect.contains del RectTransfrom parte dal presupposto che l'oggetto sia sullo 0/0 per via degli anchor(credo)
		return Math.Abs(pos.x-transform.position.x)<(GetComponent<RectTransform>().rect.width/2) && Math.Abs(pos.y-transform.position.y)<(GetComponent<RectTransform>().rect.height/2);
	}
	public virtual bool MoveItem(InventoryItem i){
		if (attached == null){
			i.slot.Empty();
			i.Rebase(this);
			return true;
		}
		else {
			if (i.slot.GetType() == typeof(InventorySlot)){
				if (i.attached.canStack && attached.attached == i.attached){
					i.SetQuantity(attached.AddQuantity(i.qty));
					return false;
				}
				else {
					
					attached.Rebase(i.slot);
					i.Rebase(this);
				}
				
				return true;
			}
			else if (i.slot.GetType() == typeof(WeaponSlot)){
				if (attached.attached.GetType() == typeof(Weapon)){
					attached.Rebase(i.slot);
					i.Rebase(this);
					return true;
				}
			}
			return false;
		}
		
	}
	public void SpawnItem(Item i){
		attached = Instantiate(Inventory.I.itemTemplate,Vector3.zero,Quaternion.identity,transform).GetComponent<InventoryItem>();
		attached.Rebase(this);
		attached.Setup(i);
	}
	public virtual void Empty(){
		attached = null;
	}
	public virtual void Clear(){
		if (attached != null){
			Destroy(attached.gameObject);
			attached = null;
		}
	}
}
