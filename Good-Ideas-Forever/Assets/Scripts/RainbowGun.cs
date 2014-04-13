using UnityEngine;
using System.Collections.Generic;

public class RainbowGun : Weapon {

	public Texture [] nyan;
	LineRenderer rend;
	int count = 0;
	int fram = 0;
	public int animSpeed = 5;
	public int frameDuration = 100;
	int secondcounter = 0;
	bool nool = false;

	// Use this for initialization
	protected override void Start () {
		rend = creator.GetComponent<LineRenderer>();
		rend.enabled = false;
		this.Power = 1;
		this.Health = 999999999;
		this.PropertyToHit = "Peace";
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}
	public override EnemyShip[] GetTargets (int x, int y, Direction d)
	{
		GameState gs = GameState.instance;
		Ship[] possibleTargets = gs.GetShipsFrom (x, y, d);
		List<EnemyShip> targets = new List<EnemyShip> ();
		if (possibleTargets.Length > 0)
		{
			foreach (Ship s in possibleTargets)
				if (s is EnemyShip)
					if (!((EnemyShip)s).IsPacified)
						targets.Add ((EnemyShip)s);
		}
		return targets.ToArray ();
	}
	public override Direction ValidFiringDirections {
		get 
		{
			return Direction.East | Direction.North | Direction.West | Direction.South;
		}
	}
	public override void Fire ()
	{
		AudioHelper.CreatePlayAudioObject(BaseManager.instance.sfx2);
		Vector3 farthestTarget = Vector3.zero;
		float distance = 0;
		float tempDistance;
		base.Fire ();
		foreach (EnemyShip s in this.GetTargets()) 
		{
			tempDistance = (s.gameObject.transform.position-creator.transform.position).magnitude;
			if(tempDistance > distance)
			{
				distance = tempDistance;
				farthestTarget = s.gameObject.transform.position;
			}
		}
		Debug.Log(farthestTarget);
		Vector3 tVect = new Vector3(farthestTarget.x,farthestTarget.y,-1);
		Vector3 pVect = new Vector3(creator.transform.position.x,creator.transform.position.y,-1);
		rend.SetPosition(0,pVect);
		rend.SetPosition(1,tVect);
		nool = true;
		rend.enabled = true;
	}
	void FixedUpdate()
	{
		if(nool)
		{
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
