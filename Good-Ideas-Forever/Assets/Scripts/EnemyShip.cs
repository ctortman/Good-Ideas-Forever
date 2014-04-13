using UnityEngine;
using System.Collections;
using System;

public class EnemyShip : Ship {
	protected int _health = 0;
	protected int _peace = 0;
	protected int _maxHealth = 3;
	
	// Use this for initialization
	void Start () 
	{
		this._maxHealth = 3;
		this._health = this._maxHealth;
		this._peace = 0;
		
		GameObject weap = GameObject.Instantiate(weaponPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		this.Weapons.Add (weap.GetComponent<BaseEnemyGun>());
		
		this.CurrentWeapon = this.Weapons [0].gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public NinjaForce Side 
	{
		get; 
		set;
	}
	public int Health;
	
	public void setHealth(int value) 
	{ 
		int delta = value - this._health;
		this._health = value;
		if (delta < 0)
		{
			this.Peace -= value;
		}
	}
	public int Peace 
	{
		get { return _peace; }
		set
		{
			if (value <= 0)
				this._peace = 0;
			else 
				this._peace = value;
		}
	}

	public int getGoal (){
		//GetGoal will return -1, 0 or 1 depending on the movement that should be made by the AI
		bool above = true;
		bool below = true;
		EnemyShip target1 = new EnemyShip ();
		EnemyShip target2 = new EnemyShip ();
		//Step 1: Check if there are ships above
		if (GameState.instance.DoesSpaceContainObject (this.StartX, this.StartY - 1)) 
		{
			above = false;
		}
		if (GameState.instance.DoesSpaceContainObject (this.StartX, this.StartY + this.Length)) 
		{
			below = false;
		}

		if(above){
			target1 = getClosest (1);
		}
		else if(below){
			target2 = getClosest (-1);
		}
		else
		{
			//If neither above or below were true, then we can't move
			return 0;
		}
		if(target1 == target2)
		{
			//Ships are the same. This can only be if they're on our current row
			return 0;
		}
		else
		{
			int north = target1.StartY;
			int south = target2.StartY;
			if (north < south) 
			{
				//North is closer
				return -1;
			} 
			else if (south < north) 
			{
				//South is closer
				return 1;
			}
			else
			{
				//North = South
				System.Random r = new System.Random();
				int nextValue = r.Next(1, 100);
				if(nextValue > 49)
				{
					return -1;
				}
				else
				{
					return 1;
				}
			}							
		}
	}
	public EnemyShip getClosest(int direction)
	{
		EnemyShip[] targets = this.CurrentWeapon.GetComponent<Weapon>().GetTargets(this.StartX, this.StartY);
		if(targets.Length > 0)
		{
			//We have enemies in our row!
			return targets [0];
		}
		else
		{
			//Given direction, we're either checking up or down
			if(direction > 0)
			{
				//Going Up!
				for(int myY = StartY; myY >= 0; myY--)
				{
					targets = this.CurrentWeapon.GetComponent<Weapon>().GetTargets(this.StartX, this.StartY);
					if(targets.Length > 0) 
					{
						//We have enemies in the current row!
						return targets [0];
					}
				}
				//Found no targets in the Up direction
				return null;
			}
			else if (direction < 0)
			{
				//Going Down!
				for(int myY = StartY; myY < GameState.instance.BoardHeight; myY++)
				{
					targets = this.CurrentWeapon.GetComponent<Weapon>().GetTargets(this.StartX, this.StartY);
					if(targets.Length > 0)
					{
						//We have enemies in the current row!
						return targets [0];
					}
				}
				//Found no targets in the down direction
				return null;
			}
			else 
			{
				//Direction is 0, so we can't move.  We've already checked if there was a target in a row and there wasn't.  NULL!
				return null;
			}
		}
	}
	public bool IsPacified
	{
		get 
		{
			return this.Peace >= this.Health;
		}
	}
	public bool IsDead 
	{
		get 
		{
			return this.Health <= 0;
		}
	}
	public void MoveAndShoot()
	{
		int goal = this.getGoal ();
		if (goal != 0)
		{
			this.Move(this.StartX, this.StartY + goal);
		}
		this.CurrentWeapon.GetComponent<Weapon>().Fire();
	}
	public override Direction ValidMovementDirections {
		get 
		{
			Direction answer = Direction.None;
			if (GameState.instance.IsMoveValid(this, this.StartX, this.StartY - 1))
				answer |= Direction.North;
			if (GameState.instance.IsMoveValid(this, this.StartX, this.StartY + 1))
				answer |= Direction.South;
			return answer;
		}
	}
	
	public void MoveToPacifiedLane()
	{
		GameState gs = GameState.instance;
		if (!this.IsInPacifiedLane) 
		{
			if (gs.GetWidthIndex(this.StartX) == 0)
			{
				if (gs.IsMoveValid(this, 1, this.StartY))
				{
					this.Move(1, this.StartY);
				}
			}
			else if (gs.GetWidthIndex(this.StartX) == gs.BoardWidth - 1)
			{
				if (gs.IsMoveValid(this, gs.BoardWidth - 1, this.StartY))
				{
					this.Move(gs.BoardWidth - 1, this.StartY);
				}
			}
			else
			{
				bool[] possibilities = new bool[2];
				possibilities[0] = gs.IsMoveValid(this, gs.BoardWidth - 1, this.StartY);
				possibilities[1] = gs.IsMoveValid(this, gs.BoardWidth + 1, this.StartY);
				if (possibilities[0] && possibilities[1])
				{
					System.Random r = new System.Random();
					if (r.Next(0,1) == 1)
					{
						this.Move(gs.BoardWidth - 1, this.StartY);
					}
					else
					{
						this.Move(gs.BoardWidth + 1, this.StartY);
					}
				}
				else if (possibilities[0])
				{
					this.Move(gs.BoardWidth - 1, this.StartY);
				}
				else if (possibilities[1])
				{
					this.Move(gs.BoardWidth + 1, this.StartY);
				}
			}
		}
	}
	public bool IsInPacifiedLane
	{
		get { return this.StartX % 2 == 1; }
	}
}
public enum NinjaForce
{
	Left,
	Right
}
