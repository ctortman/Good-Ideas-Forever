﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	private int _health;
	private int _power;

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
	/// <summary>
	/// Gets or sets the health of the weapon changes each use.
	/// </summary>
	/// <value>The health delta.</value>
	public int HealthDelta {
		get;
		set;
	}
	/// <summary>
	/// Gets or sets the property of the enemy ships to hit.
	/// </summary>
	/// <value>The property to hit.</value>
	protected string PropertyToHit 
	{
		get;
		set;
	}
	/// <summary>
	/// Gets or sets the health of the weapon.
	/// </summary>
	/// <value>The health.</value>
	public int Health
	{
		get { return _health; }
		set { _health = value; }
	}
	/// <summary>
	/// Gets or sets the power of this weapon.
	/// </summary>
	/// <value>The power.</value>
	public int Power
	{
		get { return _power; }
		set { _power = value; }
	}
	/// <summary>
	/// Fire this weapon.
	/// </summary>
	public void Fire()
	{
		this.Health-=HealthDelta;
		foreach (EnemyShip s in this.GetTargets()) 
		{
			int value = (int)(s.GetType().GetProperty(this.PropertyToHit).GetValue(s, null));
			s.GetType().GetProperty(this.PropertyToHit).SetValue(s,value + this.Power, null);
		}
	}
	/// <summary>
	/// Gets or sets the owning ship.
	/// </summary>
	/// <value>The owning ship.</value>
	public Ship OwningShip 
	{
		get; 
		set;
	}
	/// <summary>
	/// Gets the targets available for the given weapon.
	/// </summary>
	/// <returns>The targets.</returns>
	public virtual EnemyShip[] GetTargets () { return new EnemyShip[0];}
}
