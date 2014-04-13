using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	private int _health;
	private int _power;
	public GameObject projectilePrefab;
	public GameObject creator;
	public Texture [] nyan;
	public LineRenderer rend;
	int count = 0;
	int fram = 0;
	public int animSpeed = 5;
	public int frameDuration = 100;
	bool nool = false;
	public bool rain = false;

	// Use this for initialization
	protected virtual void Start () {
		this.HealthDelta = 0;
		this.Health = 0;
		this.Power = 0;
		PlayerShip temp = creator.GetComponent<PlayerShip>();
		if(rain = true)
		{
			Debug.Log("Rain true");
			rain = true;
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
	/// <summary>
	/// Gets or sets the health of the weapon changes each use.
	/// </summary>
	/// <value>The health delta.</value>
	public int HealthDelta {
		get;
		set;
	}
	/// <summary>
	/// Gets or sets the property of the enemy ships to hit.
	/// </summary>
	/// <value>The property to hit.</value>
	public string PropertyToHit 
	{
		get;
		set;
	}
	public Direction FiringDirection 
	{
		get;
		set;
	}
	/// <summary>
	/// Gets or sets the health of the weapon.
	/// </summary>
	/// <value>The health.</value>
	public int Health
	{
		get { return _health; }
		set { _health = value; }
	}
	/// <summary>
	/// Gets or sets the power of this weapon.
	/// </summary>
	/// <value>The power.</value>
	public int Power
	{
		get { return _power; }
		set { _power = value; }
	}
	/// <summary>
	/// Fire this weapon.
	/// </summary>
	public virtual void Fire()
	{
		this.Health-=HealthDelta;
		Vector3 farthestTarget = Vector3.zero;
		float distance = 0;
		float tempDistance;
		foreach (EnemyShip s in this.GetTargets()) 
		{
			tempDistance = (s.gameObject.transform.position-creator.transform.position).magnitude;
			if(rain) 
			{
				Vector3 tVect = new Vector3(farthestTarget.x,farthestTarget.y,-1);
				Vector3 pVect = new Vector3(creator.transform.position.x,creator.transform.position.y,-1);
				rend.SetPosition(0,pVect);
				rend.SetPosition(1,tVect);
				nool = true;
				rend.enabled = true;
				if(tempDistance > distance)
				{
					distance = tempDistance;
					farthestTarget = s.gameObject.transform.position;
					Debug.Log("Targetting " + s.gameObject.transform.position);
					//farthestTarget = (s.gameObject.transform.position-creator.transform.position).magnitude*.95*(s.gameObject.transform.position-creator.transform.position);
					tVect = new Vector3(farthestTarget.x,farthestTarget.y,-1);
					rend.SetPosition(1,tVect);
				}
			}
			if(null != projectilePrefab)
			{
				var projpos = creator.transform.position;
				if(this is BaseEnemyGun)
					projpos.y += UnityEngine.Random.Range(-0.25f,0.25f);
				GameObject proj = GameObject.Instantiate(projectilePrefab,projpos,Quaternion.identity) as GameObject;
				Projectile pro = proj.GetComponent<Projectile>();
				pro.focus=s.gameObject;
				pro.weap = this;
			}
			else
			{
				int value = (int)(s.GetType().GetProperty(this.PropertyToHit).GetValue(s, null));
				s.GetType().GetProperty(this.PropertyToHit).SetValue(s,value + this.Power, null);
			}
			if (s.IsDead)
			{
				s.Sink();
			}
			else if(s.IsPacified)
			{
				s.MoveToPacifiedLane();
			}
		}
	}
	/// <summary>
	/// Gets or sets the owning ship.
	/// </summary>
	/// <value>The owning ship.</value>
	public Ship OwningShip 
	{
		get; 
		set;
	}
	/// <summary>
	/// Gets the targets available for the given weapon.
	/// </summary>
	/// <returns>The targets.</returns>
	public EnemyShip[] GetTargets () { return this.GetTargets (this.OwningShip.StartX, this.OwningShip.StartY); }

	public EnemyShip[] GetTargets(int x, int y)
	{
		return GetTargets (x, y, this.FiringDirection);
	}

	public virtual EnemyShip[] GetTargets(int x, int y, Direction d) 
	{
		return new EnemyShip[0];
	}

	public virtual Direction ValidFiringDirections
	{
		get
		{
			return Direction.None;
		}
	}
	void FixedUpdate()
	{
		if(rain)
		{
			if(nool)
			{
				//Debug.Log("Nool");
				Vector3 pVect = new Vector3(creator.transform.position.x,creator.transform.position.y,-1);
				rend.SetPosition(0,pVect);
				if(count%animSpeed==0)
					fram++;
				rend.material.mainTexture = nyan[fram%9];
				count++;
			}
			if(count > frameDuration)
			{
				rend.enabled = false;
				nool = false;
				count = 0;
			}
		}
	}
}
public enum Direction 
{
	None = 0x0,
	North = 0x1,
	South = 0x2,
	East = 0x4,
	West = 0x8
}
