using UnityEngine;
using System.Collections;

public class PlayerShip : Ship {

	// Use this for initialization
	void Start () {
		GameObject weap = GameObject.Instantiate(weaponPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		this.Weapons.Add (weap.GetComponent<RainbowGun>());
		this.CurrentWeapon = this.Weapons [0].gameObject;
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
