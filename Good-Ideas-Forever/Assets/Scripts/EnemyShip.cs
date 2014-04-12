using UnityEngine;
using System.Collections;

public class EnemyShip : Ship {

	// Use this for initialization
	void Start () {
		this.Health = 3;
		this.Peace = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public NinjaForce Side 
	{
		get; 
		set;
	}
	public int Health 
	{
		get;
		set;
	}
	public int Peace 
	{
		get;
		set;
	}

}
public enum NinjaForce
{
	Left,
	Right
}
