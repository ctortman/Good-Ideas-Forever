using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	private Object[,] _gameBoard;
	private List<EnemyShip> _rightShips;
	private List<EnemyShip> _leftShips;
	private PlayerShip _player;
	private int _boardHeight = 13;
	private int _boardWidth = 13;
	static public GameState instance;

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
		this._gameBoard = new Object[_boardWidth,_boardHeight];
		this._rightShips = new List<EnemyShip> ();
		this._leftShips = new List<EnemyShip> ();
		this._player = new PlayerShip ();
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	public Object[,] GameBoard 
	{ 
		get { return this._gameBoard; }
	}

	public int BoardHeight
	{
		get { return this._boardHeight;}
	}

	public int BoardWidth
	{
		get { return this._boardWidth;}
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
}