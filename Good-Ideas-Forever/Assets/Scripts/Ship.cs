using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
	List<Weapon> _weapons = new List<Weapon>();

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
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
	public Weapon CurrentWeapon 
	{
		get;
		set;
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
				return new KeyValuePair<int, int>(this.StartX, this.StartY + (this.Length/2));
			}
		}
	}
}
