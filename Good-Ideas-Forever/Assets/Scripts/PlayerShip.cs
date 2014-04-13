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
}
