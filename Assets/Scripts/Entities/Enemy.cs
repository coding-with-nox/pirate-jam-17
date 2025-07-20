using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class Enemy : Entity
{
    public override void Setup (EntityStats es){
		base.Setup(es);
		faction = Entity.Faction.enemy;
	}
}
