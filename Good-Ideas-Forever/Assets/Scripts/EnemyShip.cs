using UnityEngine;
using System.Collections;
using System;

public class EnemyShip : Ship {
	protected int _health = 0;
	protected int _peace = 0;
	private bool _sunk = false;
	public GameObject explosionPrefab;
	public Sprite peaceSprite;
	public int WaitCount = 0;
	// Use this for initialization
	void Start () 
	{
		this._health = this.MaxHealth;
		this._peace = 0;
		
		GameObject weapon = GameObject.Instantiate(weaponPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		weapon.GetComponent<Weapon>().creator = gameObject;
		Direction directionToFire = Direction.East;
		if (this.StartX > 0)
			directionToFire = Direction.West;
		this.Weapons.Add (weapon.GetComponent<Weapon>());
		this.Weapons[0].FiringDirection = directionToFire;
		this.Weapons[0].OwningShip = this;
		this.WaitCount = 0;
		this.CurrentWeaponPrefab = this.Weapons [0].gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public NinjaForce Side 
	{
		get 
		{ 
			if (this.StartX / System.Math.Abs(this.StartX) < 0)
				return NinjaForce.Left;
			else
				return NinjaForce.Right;
		}
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
		if (GameState.instance.IsMoveValidOnBoard(this, x, this.WeaponLocation.Value+delta) && GameState.instance.IsOnBoard(x, this.WeaponLocation.Value+delta))
		{
			targets1 = GameState.instance.GetShipsFrom(x, this.WeaponLocation.Value+delta, directionToShoot);
		}
		if (GameState.instance.IsMoveValidOnBoard(this, x, this.WeaponLocation.Value-delta) &&  GameState.instance.IsOnBoard(x, this.WeaponLocation.Value-delta))
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
		if (this.WaitCount > 0)
			this.WaitCount--;
		else
		{
			if (this.IsOnBoard && !this.IsDead)
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
						//EnemyShip s = this;
						//Debug.Log(string.Format("StartX:{0} StartY: {1} Health:{2} Peace:{3} InLane:{4}",s.StartX, s.StartY, s.Health,s.Peace, s.IsInPacifiedLane));
						if (this.WeaponLocation.Value > (GameState.instance.BoardHeight/2))
						{
							this.TryMoveOffBoard(this.StartX, this.StartY+1);
						}
						else 
						{
							this.TryMoveOffBoard(this.StartX, this.StartY-1);
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
			else 
			{
				this.Sink();
			}
		}
	}
	public override Direction ValidMovementDirections {
		get 
		{
			Direction answer = Direction.None;
			if (this.IsPacified)
			{
				if (GameState.instance.IsMoveValidOffBoard(this, this.StartX, this.StartY - 1))
					answer |= Direction.North;
				if (GameState.instance.IsMoveValidOffBoard(this, this.StartX, this.StartY + 1))
					answer |= Direction.South;
			}
			else
			{
				if (GameState.instance.IsMoveValidOnBoard(this, this.StartX, this.StartY - 1))
					answer |= Direction.North;
				if (GameState.instance.IsMoveValidOnBoard(this, this.StartX, this.StartY + 1))
					answer |= Direction.South;
			}
			return answer;
		}
	}
	
	public bool IsOnBoard
	{
		get
		{
			bool answer = false;
			for (int i = 0; i < this.Length; i++)
			{
				if (GameState.instance.IsOnBoard(this.StartX, this.StartY + i))
				{
					answer = true;
					break;
				}
			}
			return answer;
		}
	}
	
	public void MoveToPacifiedLane()
	{
		this.WaitCount += 1;
		GameState gs = GameState.instance;
		gameObject.GetComponent<SpriteRenderer>().sprite = peaceSprite;
		GameState.instance.PacificationScore += GameState.instance.PacifiedShipBenefit;
		if (!this.IsInPacifiedLane) 
		{
			if (gs.GetWidthIndex(this.StartX) == 0)
			{
				if (gs.IsMoveValidOffBoard(this, this.StartX + 1, this.StartY))
				{
					this.Move(this.StartX + 1, this.StartY);
				}
			}
			else if (gs.GetWidthIndex(this.StartX) == gs.BoardWidth - 1)
			{
				if (gs.IsMoveValidOffBoard(this, this.StartX - 1, this.StartY))
				{
					this.Move(this.StartX - 1, this.StartY);
				}
			}
			else
			{
				bool[] possibilities = new bool[2];
				possibilities[0] = gs.IsMoveValidOffBoard(this, this.StartX - 1, this.StartY);
				possibilities[1] = gs.IsMoveValidOffBoard(this, this.StartX + 1, this.StartY);
				if (possibilities[0] && possibilities[1])
				{
					System.Random r = new System.Random();
					if (r.Next(0,1) == 1)
					{
						this.Move(this.StartX - 1, this.StartY);
					}
					else
					{
						this.Move(this.StartX + 1, this.StartY);
					}
				}
				else if (possibilities[0])
				{
					this.Move(this.StartX - 1, this.StartY);
				}
				else if (possibilities[1])
				{
					this.Move(this.StartX + 1, this.StartY);
				}
			}
		}
	}
	public bool IsInPacifiedLane
	{
		get { return System.Math.Abs(this.StartX) % 2 == 1; }
	}
	public void Sink()
	{
		if (!_sunk)
		{
			AudioHelper.CreatePlayAudioObject(BaseManager.instance.sfx1);
			GameState.instance.SunkScore += GameState.instance.SunkShipCost;
			int x = this.StartX;
			int y = this.StartY;
			var splode = transform.position;
			for (int i = 0; i < UnityEngine.Random.Range(6,8); i++)
			{
				splode = transform.position;
				splode.x += UnityEngine.Random.Range(-0.25f, 0.25f);
				splode.y += UnityEngine.Random.Range(-1, 0.75f);
				GameObject.Instantiate(explosionPrefab, splode, Quaternion.identity);
			}

			for (int i = 0; i < this.Length; i++)
			{
				if (GameState.instance.IsOnBoard(x, y + i))
					GameState.instance.GameBoard[GameState.instance.GetWidthIndex(x), y + i] = null;
			}
			Destroy(gameObject, 1.0f);
			_sunk = true;
		}
	}
}
public enum NinjaForce
{
	Left,
	Right
}
