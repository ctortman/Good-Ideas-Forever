using UnityEngine;
using System.Collections;

public class PlayerShip : Ship {

	// Use this for initialization
	void Start () {
		this.Weapons.Add (new RainbowGun () { Power = 1, Health = 999999999 });
		this.CurrentWeapon = this.Weapons [0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override Direction ValidMovementDirections {
		get 
		{
			Direction answer = Direction.None;
			GameState gs = GameState.instance;
			// check north

			if (gs.IsMoveValid(this, this.StartX, this.StartY - 1))
				answer |= Direction.North;
			if (gs.IsMoveValid(this, this.StartX, this.StartY + 1))
				answer |= Direction.South;
			if (gs.IsMoveValid(this, this.StartX - 1, this.StartY))
				answer |= Direction.West;
			if (gs.IsMoveValid(this, this.StartX + 1, this.StartY))
				answer |= Direction.East;
			return answer;
		}
	}
}
