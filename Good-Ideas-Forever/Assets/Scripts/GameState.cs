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
		InitializeShips(this._leftShips);
		InitializeShips(this._rightShips);
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

	void InitializeShips(List<EnemyShip> ships)
	{
		foreach(EnemyShip enemy in ships)
		{

		}
	}

	public List<Ship> getRow(int rowIndex)
	{
		List<Ship> shipRow = new List<Ship> ();
		for(int i = 0; i < _boardHeight; i++)
		{
			if(null != _gameBoard[i,rowIndex])
				shipRow.Add(_gameBoard[i,rowIndex]);
		}
		return shipRow; 
	}

	public bool DoesSpaceContainObject(int x, int y)
	{
		return true;
	}
	public Ship GetObjectFromPosition(int x, int y)
	{
		return this.Player;
	}

}