using UnityEngine;
using System.Collections;

public class PlayerShip : Ship {

	public float xMargin = 1f;		// Distance in the x axis the  can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the focus can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
	private bool m_isHoriAxisInUse = false;
	private bool m_isVertAxisInUse = false;
	
	// Use this for initialization
	void Start () {
		GameObject weapon = GameObject.Instantiate(weaponPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		this.Weapons.Add (weapon.GetComponent<Weapon>());
		this.Weapons[0].FiringDirection = Direction.East;
		this.Weapons[0].OwningShip = this;
		this.CurrentWeaponPrefab = this.Weapons [0].gameObject;
	}
	
	void FixedUpdate() {
		
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		float tempX = this.StartX;
		float tempY = this.StartY;
		if( h != 0.0f)
		{
			if(m_isHoriAxisInUse == false)
			{
				if  (((tempX+h) < maxXAndY.x) && ((tempX+h) > minXAndY.x))
					tempX += h;
				m_isHoriAxisInUse = true;
			}
		}
		if( h == 0.0f)
		{
			m_isHoriAxisInUse = false;
		} 
		if( v != 0.0f)
		{
			if(m_isVertAxisInUse == false)
			{
				if  (((tempY+v) < maxXAndY.y) && ((tempY+v) > minXAndY.y))
					tempY += v;
				m_isVertAxisInUse = true;
			}
		}
		if( v == 0.0f)
		{
			m_isVertAxisInUse = false;
		} 
		Vector2 tempVector = new Vector2(tempX,tempY);
		this.Move((int)tempX, (int)tempY);
		gameObject.transform.position = tempVector;
	}
	
	public override Direction ValidMovementDirections {
		get 
		{
			Direction answer = Direction.None;
			GameState gs = GameState.instance;
			// check north

			if (gs.IsMoveValid(this, this.StartX, this.StartY - 1))
				answer |= Direction.North;
			if (gs.IsMoveValid(this, this.StartX, this.StartY + 1))
				answer |= Direction.South;
			if (gs.IsMoveValid(this, this.StartX - 1, this.StartY))
				answer |= Direction.West;
			if (gs.IsMoveValid(this, this.StartX + 1, this.StartY))
				answer |= Direction.East;
			return answer;
		}
	}
}
