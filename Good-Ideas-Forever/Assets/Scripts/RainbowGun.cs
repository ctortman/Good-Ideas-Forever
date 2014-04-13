using UnityEngine;
using System.Collections.Generic;

public class RainbowGun : Weapon {

	// Use this for initialization
	protected override void Start () {
		this.Power = 1;
		this.Health = 999999999;
		this.PropertyToHit = "Peace";
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}
	public override EnemyShip[] GetTargets (int x, int y, Direction d)
	{
		GameState gs = GameState.instance;
		Ship[] possibleTargets = gs.GetShipsFrom (x, y, d);
		List<EnemyShip> targets = new List<EnemyShip> ();
		if (possibleTargets.Length > 0)
		{
			foreach (Ship s in possibleTargets)
				if (s is EnemyShip)
					if (!((EnemyShip)s).IsPacified)
						targets.Add ((EnemyShip)s);
		}
		return targets.ToArray ();
	}
	public override Direction ValidFiringDirections {
		get 
		{
			return Direction.East | Direction.North | Direction.West | Direction.South;
		}
	}
}
