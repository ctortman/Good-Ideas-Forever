using UnityEngine;
using System.Collections.Generic;

public class RainbowGun : Weapon {

	// Use this for initialization
	protected override void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}
	public override EnemyShip[] GetTargets (int x, int y)
	{
		List<EnemyShip> targets = new List<EnemyShip> ();

		return targets.ToArray ();
	}
}
