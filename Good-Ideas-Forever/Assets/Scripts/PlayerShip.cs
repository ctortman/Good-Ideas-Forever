using UnityEngine;
using System.Collections;

public class PlayerShip : Ship {

	// Use this for initialization
	void Start () {
		base.DefaultWeapon = new RainbowGun ();
		base.Weapons.Add (base.DefaultWeapon);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
