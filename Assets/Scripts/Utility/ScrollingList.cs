using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public abstract class ScrollingList<T>:MonoBehaviour  where T:MovingObject
{
	public List<T> itemList = new();
	public float yDistance, xOffset;
	public GameObject itemContainer;
	public Scrollbar slider;
    public void ResetButtons(){
		foreach (T t in itemList){
			DeleteObject(t);
		}
		itemList.Clear();
	}
	public void OrderItems (){
		/*
		float diff = (itemList.Count*yDistance)-(itemContainer.GetComponent<RectTransform>().rect.height-20);
		if (diff <0){
			diff = 0;
		}
		float offset = diff*slider.value;
		foreach (T t in itemList){
			t.Move(new Vector3(xOffset,offset,0));
			offset -= t.gameObject.GetComponent<RectTransform>().rect.height+20;
		}
		*/
		//float offset = (itemList.Count*yDistance*slider.value)-itemContainer.GetComponent<RectTransform>().rect.height-(itemContainer.GetComponent<RectTransform>().rect.height/2);
		float offset = (itemList.Count*yDistance*slider.value)-(itemContainer.GetComponent<RectTransform>().rect.height/2);
		if (offset <0){
			offset = 0;
		}
		int c = 0;
		foreach (T t in itemList){
			//t.Move(new Vector3(xOffset,(c*yDistance)-offset,0));
			t.Move(new Vector3(xOffset,(c*(t.gameObject.GetComponent<RectTransform>().rect.height+yDistance))-offset,0));
			//offset -= t.gameObject.GetComponent<RectTransform>().rect.height+20;
			c++;
		}
	}
	public abstract void DeleteObject(T t);
}
public abstract class ScrollingListMenu<T>:Menu  where T:MovingObject
{
	public List<T> itemList = new();
	public float yDistance, xOffset;
	[SerializeField] bool fixedDistance;
	public GameObject itemContainer;
	public Scrollbar slider;
	public GameObject buttonPrefab;
    public void ResetButtons(){
		foreach (T t in itemList){
			DeleteObject(t);
		}
		itemList.Clear();
	}
	public void OrderItems (){
		if (itemList.Count == 0){
			return;
		}
		//float offset = (itemContainer.GetComponent<RectTransform>().rect.height)-(yDistance)-(itemList.Count*yDistance*slider.value);
		//float offset = (yDistance+buttonPrefab.GetComponent<RectTransform>().rect.height/2)-(itemList.Count*yDistance*slider.value);
		float initialPosition;
		float offset = yDistance+buttonPrefab.GetComponent<RectTransform>().rect.height;
		if (fixedDistance){
			if ((itemList.Count*(yDistance))-itemContainer.GetComponent<RectTransform>().rect.height<0){
				initialPosition = (-offset/2)+itemContainer.GetComponent<RectTransform>().rect.height/2;
			}
			else {
				initialPosition = (-offset/2)+itemContainer.GetComponent<RectTransform>().rect.height/2-((yDistance)-itemContainer.GetComponent<RectTransform>().rect.height)*slider.value;
			}
		}
		else {
			
			if ((itemList.Count*(buttonPrefab.GetComponent<RectTransform>().rect.height+yDistance))-itemContainer.GetComponent<RectTransform>().rect.height<0){
				initialPosition = (-offset/2)+itemContainer.GetComponent<RectTransform>().rect.height/2;
			}
			else {
				initialPosition = (-offset/2)+itemContainer.GetComponent<RectTransform>().rect.height/2+((itemList.Count*(buttonPrefab.GetComponent<RectTransform>().rect.height+yDistance))-itemContainer.GetComponent<RectTransform>().rect.height)*slider.value;
			}
		}
		/*
		if (offset <0){
			offset = 0;
		}
		*/
		int c = 0;
		foreach (T t in itemList){
			//t.Move(new Vector3(xOffset,(c*yDistance)-offset,0));
			t.Move(new Vector3(xOffset,(initialPosition-(c*offset)),0));
			c++;
		}
	}
	public abstract void DeleteObject(T t);
}