﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	private Object[,] _gameBoard;
	private List<EnemyShip> _rightShips;
	private List<EnemyShip> _leftShips;
	private PlayerShip _player;
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
		this._gameBoard = new Object[12,12];
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