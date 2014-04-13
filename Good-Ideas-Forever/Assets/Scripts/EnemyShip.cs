using UnityEngine;
using System.Collections;
using System;

public class EnemyShip : Ship {
	protected int _health = 0;
	protected int _peace = 0;
	
	// Use this for initialization
	void Start () 
	{
		this._health = this.MaxHealth;
		this._peace = 0;
		
		GameObject weapon = GameObject.Instantiate(weaponPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		Direction directionToFire = Direction.East;
		if (this.StartX > 0)
			directionToFire = Direction.West;
		this.Weapons.Add (weapon.GetComponent<Weapon>());
		this.Weapons[0].FiringDirection = directionToFire;
		this.Weapons[0].OwningShip = this;
		
		this.CurrentWeaponPrefab = this.Weapons [0].gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public NinjaForce Side 
	{
		get; 
		set;
	}
	public int MaxHealth;

	public int Health
	{
		get { return _health; }
		set 
		{ 
			int delta = value - this._health;
			this._health = value;
			if (delta < 0)
			{
				this.Peace -= value;
			}
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
	public int getGoal()
	{
		int side = this.StartX;
		Direction directionToShoot = Direction.West;
		if (side < 0)
		{
			directionToShoot = Direction.East;
		}

		Ship[] targets1;
		targets1 = GameState.instance.GetShipsFrom(this.WeaponLocation.Key, this.WeaponLocation.Value, directionToShoot);
		if (targets1.Length != 0)
			return 0;
		else
		{
			return getGoalRecurse(this.WeaponLocation.Key, 1, directionToShoot);
		}
	}
	int getGoalRecurse(int x, int delta, Direction directionToShoot)
	{
		Ship[] targets1 = null;
		Ship[] targets2 = null;
		if (GameState.instance.IsMoveValid(this, x, this.WeaponLocation.Value+delta) && GameState.instance.IsOnBoard(x, this.WeaponLocation.Value+delta))
		{
			targets1 = GameState.instance.GetShipsFrom(x, this.WeaponLocation.Value+delta, directionToShoot);
		}
		if (GameState.instance.IsMoveValid(this, x, this.WeaponLocation.Value-delta) &&  GameState.instance.IsOnBoard(x, this.WeaponLocation.Value-delta))
		{
			targets2 = GameState.instance.GetShipsFrom(x, this.WeaponLocation.Value-delta, directionToShoot);
		}
		if (targets1 != null && targets2 != null)
		{
			if (targets1.Length == 0 && targets2.Length == 0)
			{
				return getGoalRecurse(x, delta+1, directionToShoot);
			}
			else if (targets1.Length == 0)
			{
				return -1;
			}
			else if (targets2.Length == 0)
			{
				return 1;
			}
			else if (targets1.Length > targets2.Length)
			{
				return 1;
			}
			else 
			{
				return -1;
			}
		}
		else if (targets1 != null)
		{
			return 1;
		}
		else if (targets2 != null)
		{
			return -1;
		}
		return 0;
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
		if (this.IsDead)
		{
			Debug.LogError("MoveAndShoot(): Trying to move a dead ship.");
		}
		else if (this.IsPacified)
		{
			if (this.IsInPacifiedLane)
			{
				//move 1 toward the edge
				if (this.WeaponLocation.Value > (GameState.instance.BoardHeight/2))
				{
					this.TryMove(this.StartX, this.StartY+1);
				}
				else 
				{
					this.TryMove(this.StartX, this.StartY-1);
				}
			}
			else
			{
				this.MoveToPacifiedLane();
			}
		}
		else 
		{
			int goal = this.getGoal ();
			//Debug.LogError(string.Format("x:{0} y:{1} delta: {2}", this.StartX.ToString(), this.StartY.ToString(), goal.ToString()));
			if (goal != 0)
			{
				this.Move(this.StartX, this.StartY + goal);
			}
			this.CurrentWeaponPrefab.GetComponent<Weapon>().Fire();
		}
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
	public void Sink()
	{
	
	}
}
public enum NinjaForce
{
	Left,
	Right
}
