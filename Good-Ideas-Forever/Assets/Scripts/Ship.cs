using UnityEngine;
using System.Collections.Generic;
using System;

public class Ship : MonoBehaviour {

	private List<Weapon> _weapons = new List<Weapon>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public int Length 
	{
		get;
		set;
	}
	public int StartX
	{
		get;
		set;
	}
	public int StartY
	{
		get;
		set;
	}
	public List<Weapon> Weapons
	{
		get { return _weapons; }
	}
	public Weapon DefaultWeapon 
	{
		get;
		set;

	}
	public void Move(int newStartX, int newStartY)
	{

	}
	public void Fire(Weapon weapon)
	{
		weapon.Fire ();
	}

}
