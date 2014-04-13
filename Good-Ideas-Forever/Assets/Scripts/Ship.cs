﻿using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
	List<Weapon> _weapons = new List<Weapon>();
	public int Length;
	public int StartX;
	public int StartY;
	public GameObject weaponPrefab;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public GameObject CurrentWeaponPrefab;

	public Weapon CurrentWeapon
	{
		get { return this.CurrentWeaponPrefab.GetComponent<Weapon>(); }
	}	
	public List<Weapon> Weapons
	{
		get { return _weapons; }
	}
	public void Move(int newStartX, int newStartY)
	{
		GameState.instance.MoveObject (this.StartX, this.StartY, newStartX, newStartY);
	}
	public virtual Direction ValidMovementDirections
	{
		get
		{
			return Direction.None;
		}
	}


	public KeyValuePair<int, int> WeaponLocation
	{
		get 
		{
			if (this.Length == 1)				
				return new KeyValuePair<int, int>(this.StartX,this.StartY);
			else
			{
				return new KeyValuePair<int, int>(this.StartX, this.StartY + ((this.Length - 1)/2));
			}
		}
	}
}
