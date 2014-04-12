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
		GameState gs = GameState.instance;
		List<Ship> possibleTargets;
		int delta = 1;
		if (this.FiringDirection == Direction.South || this.FiringDirection == Direction.East)
			delta = -1;
		if (this.FiringDirection == Direction.North || this.FiringDirection == Direction.South) 
			possibleTargets = gs.getColumn (x);		
		else 
			possibleTargets = gs.getRow(y);
		
		
		Ship current = possibleTargets [0];
		
		
		return targets.ToArray ();
	}

}
