using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	private Ship[,] _gameBoard;
	private List<EnemyShip> _rightShips;
	private List<EnemyShip> _leftShips;
	private PlayerShip _player;
	static public GameState instance;
	private int _boardHeight = 13;
	private int _boardWidth = 13;


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
		this._player = new PlayerShip ();
		InitializeShips(this._leftShips,_boardHeight*2);
		InitializeShips(this._rightShips,_boardHeight*((_boardWidth-1)/2+2));
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	public Object[,] GameBoard 
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
			for (int i = 0; i < enemy.Length; i++)
			{
				_gameBoard[placementIndex / _boardHeight , placementIndex % _boardHeight + i] = enemy;
			}
			if ((placementIndex + enemy.Length) % _boardHeight != 0)
				placementIndex = placementIndex + enemy.Length + 1;
			else
				placementIndex = placementIndex + enemy.Length;
		}
	}

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

	public bool DoesSpaceContainObject(int x, int y)
	{
		return GetObjectFromPosition (x, y) != null;
	}
	public Ship GetObjectFromPosition(int x, int y)
	{
		return this.GameBoard[GetWidthIndex(x), y];
	}

	public int GetWidthIndex(int x)
	{
		return ((_boardWidth - 1)/2 + x);
	}

	public Ship[] GetShipsFrom(int x, int y, Direction d)
	{
		int column = GetWidthIndex (x);


		return new Ship[0];
	}
	public void MoveObject(int x1, int x2, int y1, int y2)
	{
		int column1 = GetWidthIndex (x2);
		int column2 = GetWidthIndex (x2);

	}
}