using UnityEngine;
using System;
[Serializable]
public class DamageValue
{
    public enum Type{
		blunt,
		piercing,
		slashing,
		fire,
		cold,
		acid,
		lightning,
		force
	}
	public Type type;
	public int val;
	public float slowsFor, slowsPerc;
	
}
