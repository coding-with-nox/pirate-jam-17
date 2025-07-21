using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class InventoryItem : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static InventoryItem moving;
	
	bool dragging;
	InventorySlot slot;
	
	Item attached;
	
    public void OnBeginDrag(PointerEventData data) { 
		dragging = true;
	}

    public void OnDrag(PointerEventData data)
    {
		if (dragging){
			transform.position = new Vector3(data.position.x, data.position.y, 0);
			
		}
		
    }
	public void Rebase (InventorySlot ins){
		if (ins != null){
			transform.SetParent(ins.transform);
		}
		slot = ins;
		ins.attached = this;
		transform.localPosition = Vector3.zero;
	}
    public void OnEndDrag(PointerEventData data)
    {
		dragging = false;
		if (!Inventory.I.DropItem(this,data.position)){
			transform.localPosition = Vector3.zero;
		}
		
    }
	public void Setup (Item i){
		attached = i;
	}
}
