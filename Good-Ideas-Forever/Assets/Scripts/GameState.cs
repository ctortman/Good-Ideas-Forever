using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	private Object[,] _gameboard;
	private List<EnemyShip> _rightShips;
	private List<EnemyShip> _leftShips;
	private PlayerShip _player;

	// Use this for initialization
	void Start () 
	{
		this._gameboard = new Object[12,12];

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

}
