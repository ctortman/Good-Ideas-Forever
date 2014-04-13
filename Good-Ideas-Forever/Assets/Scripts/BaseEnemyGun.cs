using UnityEngine;
using System.Collections.Generic;

public class BaseEnemyGun : Weapon {

	// Use this for initialization
	protected override void Start () {
		this.Power = -1;
		this.Health = 999999999;
		this.PropertyToHit = "Health";
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}
	public override EnemyShip[] GetTargets (int x, int y, Direction d)
	{
		GameState gs = GameState.instance;
		Ship[] possibleTargets = gs.GetShipsFrom (x, y, d);
		if (possibleTargets.Length == 0) 
		{
			return new EnemyShip [0];
		}
		else 
		{
			Ship current = possibleTargets[0];
			if (((EnemyShip)current))
			{
				EnemyShip[] targets = new EnemyShip[1];
				targets[0] = (EnemyShip)current;
				return targets;
			}
			else
				return new EnemyShip [0];
		}
	}
	public override Direction ValidFiringDirections {
		get 
		{
			if (this.OwningShip.StartX > 0)
			{
				return Direction.East;
			}
			else
			{
				return Direction.West;
			}
		}
	}
}
