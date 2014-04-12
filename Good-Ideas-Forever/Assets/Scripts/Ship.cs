using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

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
	public Weapon CurrentWeapon {
		get;
		set;
	}
	public void Move(int newStartX, int newStartY)
	{

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
