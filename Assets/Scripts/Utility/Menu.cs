using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public abstract class Menu:MonoBehaviour
{
	[Serializable]
	public enum Position {
		Right,
		Detached
	}
	public enum Function {
		unit,
		build,
		research,
	}
	
	[SerializeField]bool defMenu, battleMenu;
	[SerializeField]Position pos;
	[SerializeField] Function fun;
	static Dictionary<Position,Menu> active = new();
	static Dictionary<Position,Menu> def = new();
	static List<Menu> menuList = new();
	void Start(){
		menuList.Add(this);
		if (defMenu){
			Activate();
			if (!def.ContainsKey(pos)){
				def.Add(pos,this);
			}
		}
		else {
			gameObject.SetActive(false);
		}
	}
	public static void SetActive(Function f, bool flag = true){
		foreach (Menu m in menuList){
			if (m.fun == f)
			{
				m.SetActive(flag);
				return;
			}
		}
		Debug.Log ("Could not find a suitable UI menu");
	}
	public void SetActive(bool flag){
		if (flag){
			Activate();
		}
		else {
			Deactivate();
		}
		if (active[pos] == null && def.ContainsKey(pos)){
			def[pos].Activate();
		}
	}
	void Activate (){
		if (!active.ContainsKey(pos) || active[pos] != this){
			gameObject.SetActive(true);
			SetupStandard();
			if (active.ContainsKey(pos)){
				if (active[pos] != null){
					active[pos].Deactivate();
				}
				active[pos] = this;
			}
			else {
				active.Add(pos,this);
			}
		}
	}
	void Deactivate(){
		gameObject.SetActive(false);
		if (active.ContainsKey(pos)){
			active[pos] = null;
		}
		else{
			active.Add(pos,null);
		}
	}
	
	public abstract void SetupStandard();
}
