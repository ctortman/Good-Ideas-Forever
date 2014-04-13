using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {
	
	private Ship[,] _gameBoard;
	private List<EnemyShip> _rightShips;
	private List<EnemyShip> _leftShips;
	public GameObject leftShipPrefab;
	public GameObject rightShipPrefab;
	private PlayerShip _player;
	static public GameState instance;
	private int _boardHeight = 13;
	private int _boardWidth = 13;
	public int teamSize = 6;
	private int counter = 250;
	public int SunkShipCost = -2;
	public int PacifiedShipBenefit = 1;
	public int PacificationScore = 0;
	public int SunkScore = 0;
	public bool IsPlayerTurn = true;
	
	void Awake ()
	{
		if (null != instance)
		{
			Debug.LogError("A second GameState has been created!");
		}
		instance = this;
	}
	
	// Use this for initialization
	void Start () 
	{
		this._gameBoard = new Ship[_boardWidth,_boardHeight];
		this._rightShips = new List<EnemyShip> ();
		this._leftShips = new List<EnemyShip> ();
		//this._player = new PlayerShip ();
		CreateShips();
		InitializeShips(this._leftShips,_boardHeight*2);
		InitializeShips(this._rightShips,_boardHeight*((_boardWidth-1)/2+2));
		this.IsPlayerTurn = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void FixedUpdate ()
	{
		if (!this.IsPlayerTurn)
		{
			this.AITakeTurn();
			this.IsPlayerTurn = true;
		}
	}
	
	public Ship[,] GameBoard 
	{ 
		get { return this._gameBoard; }
	}
	
	public List<EnemyShip> RightShips 
	{
		get { return _rightShips; }
	}
	
	public List<EnemyShip> LeftShips
	{
		get { return _leftShips; }
	}
	public PlayerShip Player 
	{
		get { return _player; }
	}
	public int BoardHeight
	{
		get { return this._boardHeight; }
	}
	public int BoardWidth
	{
		get { return _boardWidth; }
	}
	
	void CreateShips()
	{
		for (int i = 0; i < teamSize; i++)
		{
			GameObject newShip = GameObject.Instantiate(rightShipPrefab,Vector3.zero, Quaternion.identity) as GameObject;
			EnemyShip newEnemy = newShip.GetComponent<EnemyShip>();
			if(newEnemy != null)
				_rightShips.Add(newEnemy);
		}
		for (int i = 0; i < teamSize; i++)
		{
			GameObject newShip = GameObject.Instantiate(leftShipPrefab,Vector3.zero, Quaternion.identity) as GameObject;
			EnemyShip newEnemy = newShip.GetComponent<EnemyShip>();
			if(newEnemy != null)
				_leftShips.Add(newEnemy);
		}
	}
	void InitializeShips (List<EnemyShip> ships, int startPosition)
	{
		int placementIndex = 0;
		System.Random r = new System.Random (unchecked(System.DateTime.Now.Ticks.GetHashCode ()));
		foreach (EnemyShip enemy in ships) {
			bool validPos = false;
			int x, y;
			while (!validPos) {
				int mod_y, mod_x;
				//Go from length-1 to boardHeight so the length can't start below the board
				//And the origin can't be higher than boardHeight-1 (non-inclusive)
				mod_y = r.Next (enemy.Length - 1, _boardHeight);
				//Start at 1 so we don't end up in the first column
				//Leave right side at boardWidth-1/2 because it's non-inclusive
				mod_x = r.Next (1, ((_boardWidth - 1) / 2));
				placementIndex = (mod_x * _boardHeight) + (mod_y + startPosition);
				
				if ((placementIndex % _boardHeight + enemy.Length) > _boardHeight) {
					placementIndex = (placementIndex / _boardHeight + enemy.Length) * _boardHeight;
				}
				x = placementIndex / _boardHeight;
				y = placementIndex % _boardHeight;
				if ((!isValidStartX (x)) || (!(isValidStartY (y))))
					continue;
				if (DoesSpaceContainObject (GetWidthPosition(x), y))
					continue;
				if (DoesSpaceContainObject (GetWidthPosition(x), y + (enemy.Length - 1)))
					continue;
				
				//validPos is verified
				validPos = true;
				for (int i = 0; i < enemy.Length; i++)
				{
					_gameBoard[x , y + i] = enemy;
				}
				enemy.StartX = GetWidthPosition(x);
				enemy.StartY = y;
				Vector3 tempVect = new Vector3 (GetWidthPosition(x),y,0);
				enemy.gameObject.transform.position = tempVect;
				enemy.focus = tempVect;
			}					
		}
	}
	bool isValidStartX(int x){
		return (x != 0) && (x%2 == 0) && (System.Math.Abs(x) != (_boardWidth-1)/2);
	}
	
	bool isValidStartY(int y){
		return (y > 0) && (y < _boardHeight);
	}
	/*
	void InitializeShips(List<EnemyShip> ships, int startPosition)
	{
		int placementIndex = startPosition;
		foreach (EnemyShip enemy in ships)
		{
			if ((placementIndex % _boardHeight + enemy.Length) > _boardHeight)
				{
					placementIndex = (placementIndex / _boardHeight + 1) * _boardHeight;
				}
				// Checks to see if the index interating through the board is placing a ship beyond the valid placement area
				if ((placementIndex >= (_boardHeight * _boardWidth) && startPosition > ((_boardWidth - 1) / 2 * _boardHeight))
				    || (placementIndex >= ((_boardWidth - 1) / 2 * _boardHeight) && startPosition < ((_boardWidth - 1) / 2 * _boardHeight)))
				{
					Debug.LogError("Attempting to place ships out of bounds");
					break;
				}
			int x = placementIndex / _boardHeight;
			int y = placementIndex % _boardHeight;
			for (int i = 0; i < enemy.Length; i++)
			{
				_gameBoard[x , y + i] = enemy;
			}
			enemy.StartX = GetWidthPosition(placementIndex / _boardHeight);
			enemy.StartY = placementIndex % _boardHeight;
			Vector3 tempVect = new Vector3 (GetWidthPosition(x),y,0);
			enemy.gameObject.transform.position = tempVect;
			enemy.focus = tempVect;
			if (((placementIndex + enemy.Length) % _boardHeight) != 0)
				placementIndex = placementIndex + enemy.Length + 1;
			else
				placementIndex = placementIndex + enemy.Length + _boardHeight;
		}
	}
	*/
	
	public List<Ship> getRow(int rowIndex)
	{
		int index = GetWidthIndex(rowIndex);
		List<Ship> shipRow = new List<Ship> ();
		for (int i = 0; i < _boardHeight; i++)
		{
			if(null != _gameBoard[i,index])
				shipRow.Add(_gameBoard[i,index]);
		}
		return shipRow; 
	}
	
	public List<Ship> getColumn(int columnIndex)
	{
		List<Ship> shipColumn = new List<Ship> ();
		return shipColumn;
	}
	
	public bool IsMoveValid(Ship s, int x, int y)
	{
		if( ! IsSpaceValid (s, x, y) )
		{
			return false;
		}
		for (int i = 0; i <= s.Length; i++) 
		{
			if( DoesSpaceContainObject (x, y + i) )
			{
				//Check if this Object is NOT ME
				if(s != GetObjectFromPosition(x,y+i)) //If Object is NOT ME, then it's not a valid move
					return false;
			}
			//Else condition is that Object is ME, and we're not worried about that.
		}
		//We've iterated through everything and received no falses yet.  True!
		return true;
	}
	
	public bool IsSpaceValid(Ship s, int x, int y)
	{
		int column = GetWidthIndex (x);
		
		return column >= 0 && this.BoardWidth > column && y + s.Length >= 0 && y < this.BoardHeight;
	}
	
	
	public bool DoesSpaceContainObject(int x, int y)
	{
		return GetObjectFromPosition (x, y) != null;
	}
	public Ship GetObjectFromPosition(int x, int y)
	{
		if (IsOnBoard(x, y))
			return this.GameBoard[GetWidthIndex(x), y];
		else
		{
			return null;
		}
	}
	public bool IsOnBoard (int x, int y)
	{
		int column = GetWidthIndex(x);
		return column > -1 && column < this.BoardWidth && y > -1 && y < this.BoardHeight;
	}
	public int GetWidthIndex(int x)
	{
		return ((_boardWidth - 1)/2 + x);
	}
	
	public int GetWidthPosition(int x)
	{
		return (x - (_boardWidth - 1)/2);
	}
	
	public Ship[] GetShipsFrom(int x, int y, Direction d)
	{
		//int column = GetWidthIndex (x);
		int myY = 0;
		int myX = 0;
		List<Ship> s = new List<Ship> ();
		if( d == Direction.North)
		{
			if(y == 0)
			{
				return s.ToArray();
			}
			for(myY = y-1; myY >= 0; myY--)
			{
				if(DoesSpaceContainObject(x,myY))
				{
					s.Add (GetObjectFromPosition (x, myY));
				}
			}
			return s.ToArray();
		}
		else if(d == Direction.East)
		{
			int column = GetWidthIndex(x);
			if(column<0 || column > this.BoardWidth)
			{
				return s.ToArray();
			}
			for(myX = x+1; myX <= ( ((_boardWidth - 1)/2 )); myX++)
			{
				if(DoesSpaceContainObject(myX,y))
				{
					s.Add (GetObjectFromPosition (myX, y));
				}
			}
			return s.ToArray();
		}
		else if(d == Direction.South)
		{
			if(y == this.BoardHeight - 1)
			{
				return s.ToArray();
			}
			for(myY = y+1; myY <= _boardHeight; myY++)
			{
				if(DoesSpaceContainObject(x,myY))
				{
					s.Add (GetObjectFromPosition (x, myY));
				}
			}
			return s.ToArray();
		}
		else if(d == Direction.West)
		{
			int column = GetWidthIndex(x);
			if(column<0 || column > this.BoardWidth)
				//			if(x < ( ((_boardWidth - 1)/2 )) )
			{
				return s.ToArray();
			}
			for(myX = x-1; myX >= ( ((_boardWidth - 1)/2 )*-1); myX--)
			{
				if(DoesSpaceContainObject(myX,y))
				{
					s.Add (GetObjectFromPosition (myX, y));
				}
			}
			return s.ToArray();
		}
		else
		{
			Debug.LogError("You're checking a direction that is not handled. Fail!");
		}
		return s.ToArray();
	}
	
	public void MoveObject(Ship s, int x2, int y2)
	{
		//Debug.LogError(string.Format("Move Object: x1:{0} y1:{1} x2:{2} y2:{3}", x1.ToString(), y1.ToString(), x2.ToString(), y2.ToString()));
		if (!IsMoveValid (s, x2, y2)) 
		{
			//Move is not valid
			Debug.LogError ("Move to " + x2 + ", " + y2 + " was not valid.");
		} 
		else 
		{
			int x1 = s.StartX;
			int y1 = s.StartY;
			s.StartX = x2;
			s.StartY = y2;
			int column1 = GetWidthIndex(x1);
			int column2 = GetWidthIndex(x2);
			for (int i = 0; i < s.Length; i++)
			{
				if (this.IsOnBoard(x1, y1 + i))
					this.GameBoard[column1, y1 + i] = null;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (y2 + i > -1 && y2 + i < this.BoardHeight)
				{
					if (this.IsOnBoard(x2, y2 + i))
						this.GameBoard[column2, y2 + i] = s;
				}
			}
			//whoami.gameObject.transform.position = new Vector3(whoami.StartX,whoami.StartY,0);
			s.focus = new Vector3(s.StartX,s.StartY,0);
		}
	}
	public int GetScore()
	{
		return PacificationScore + SunkScore;
	}
	
	public int GetScorePercent()
	{
		return GetScore()/(teamSize*2*PacifiedShipBenefit);
	}
	
	
	public void AITakeTurn()
	{
		foreach (EnemyShip s in _leftShips)
		{
			s.MoveAndShoot();
		}
		foreach (EnemyShip s in _rightShips)
		{
			s.MoveAndShoot();
		}
	}
}