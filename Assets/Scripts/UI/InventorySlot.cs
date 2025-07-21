using UnityEngine;
using UnityEngine.EventSystems;
using System;
[Serializable]
public class InventorySlot : MonoBehaviour
{
	public InventoryItem attached;
    public bool IsInside(Vector2 pos){
		//la funzione rect.contains del RectTransfrom parte dal presupposto che l'oggetto sia sullo 0/0 per via degli anchor(credo)
		return Math.Abs(pos.x-transform.position.x)<(GetComponent<RectTransform>().rect.width/2) && Math.Abs(pos.y-transform.position.y)<(GetComponent<RectTransform>().rect.height/2);
	}
	public bool MoveItem(InventoryItem i){
		i.Rebase(this);
		return true;
	}
	public void SpawnItem(Item i){
		attached = Instantiate(Inventory.I.itemTemplate,Vector3.zero,Quaternion.identity,transform).GetComponent<InventoryItem>();
		attached.Rebase(this);
		attached.Setup(i);
	}
	public void Empty(){
		
	}
}
