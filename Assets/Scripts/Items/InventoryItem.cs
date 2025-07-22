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
	[SerializeField]TMP_Text nameDisplay, quantityDisplay;
	public int qty;
	bool dragging;
	public InventorySlot slot;
	
	public Item attached;
	
    public void OnBeginDrag(PointerEventData data) { 
		if (Player.CanShuffleItems()){
		dragging = true;
		}
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
		if (dragging){
			dragging = false;
			if (!Inventory.I.DropItem(this,data.position)){
				transform.localPosition = Vector3.zero;
			}
		}
    }
	public void Setup (Item i, float maxPerc = 1f){
		attached = i;
		nameDisplay.SetText(i.displayedName);
		if (i.canStack){
			qty = (int)((float)i.maxStackSize*UnityEngine.Random.Range(0,maxPerc));
			if (qty == 0){
				qty++;
			}
			quantityDisplay.SetText(""+qty+"/"+i.maxStackSize);
			
		}
		else {
			quantityDisplay.SetText("");
		}
	}
	void UpdateDisplayQty (){
		if (attached.canStack){
			quantityDisplay.SetText(""+qty+"/"+attached.maxStackSize);
		}
		else {
			quantityDisplay.SetText("");
		}
	}
	public void Setup (Item i, int startingQuantity){
		attached = i;
		nameDisplay.SetText(i.displayedName);
		if (i.canStack){
			qty = startingQuantity;
			quantityDisplay.SetText(""+qty+"/"+i.maxStackSize);
			
		}
		else {
			quantityDisplay.SetText("");
		}
	}
	public int AddQuantity (int newQty){
		if (qty+newQty > attached.maxStackSize){
			int overflow = qty+newQty - attached.maxStackSize;
			qty = attached.maxStackSize;
			UpdateDisplayQty();
			return (overflow);
		}
		else {
			qty = qty+newQty;
			UpdateDisplayQty();
			return 0;
		}
	}
	public void SetQuantity(int newQty){
		if (newQty <= 0){
			slot.Clear();
		}
		else if (qty >attached.maxStackSize){
			qty =attached.maxStackSize;
		}
		else {
			qty = newQty;
		}
		UpdateDisplayQty();
	}
}
