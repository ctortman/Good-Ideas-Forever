using UnityEngine;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
	List<Weapon> _weapons = new List<Weapon>();
	public int Length;
	public int StartX;
	public int StartY;
	public GameObject weaponPrefab;
	public Vector3 focus;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float tempX;
		float tempY;
		if  (null  !=  focus)
		{
			tempX = Mathf.Lerp(gameObject.transform.position.x,focus.x,0.1f);
			tempY = Mathf.Lerp(gameObject.transform.position.y,focus.y,0.1f);
			if(Mathf.Sign(gameObject.transform.position.y-focus.y)!= (-Mathf.Sign(gameObject.transform.localScale.y)))
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,-gameObject.transform.localScale.y,gameObject.transform.localScale.z);
			gameObject.transform.position = new Vector3(tempX,tempY,gameObject.transform.position.z);
			if (Mathf.Abs(focus.x-gameObject.transform.position.x)<.05 && Mathf.Abs(focus.y-gameObject.transform.position.x)<.05 )
			{
				tempX = focus.x;
				tempY = focus.y;
				gameObject.transform.position = new Vector3(tempX,tempY,gameObject.transform.position.z);
			}
		}
	}
	
	public GameObject CurrentWeaponPrefab;

	public Weapon CurrentWeapon
	{
		get { return this.CurrentWeaponPrefab.GetComponent<Weapon>(); }
	}	
	public List<Weapon> Weapons
	{
		get { return _weapons; }
	}
	public void Move(int newStartX, int newStartY)
	{
		GameState.instance.MoveObject (this.StartX, this.StartY, newStartX, newStartY);
	}
	public virtual Direction ValidMovementDirections
	{
		get
		{
			return Direction.None;
		}
	}
	public bool TryMove(int newStartX, int newStartY)
	{
		if (GameState.instance.IsMoveValid(this, newStartX, newStartY))
		{
			Move(newStartX, newStartY);
			return true;
		}
		return false;
	}

	public KeyValuePair<int, int> WeaponLocation
	{
		get 
		{
			if (this.Length == 1)				
				return new KeyValuePair<int, int>(this.StartX,this.StartY);
			else
			{
				return new KeyValuePair<int, int>(this.StartX, this.StartY + ((this.Length - 1)/2));
			}
		}
	}
}
