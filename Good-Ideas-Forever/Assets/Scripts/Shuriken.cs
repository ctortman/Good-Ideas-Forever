using UnityEngine;
using System.Collections;

public class Shuriken : Projectile {
	
	void Awake ()
	{
		//weap = gameObject.GetComponent<Weapon>();
	}
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(focus!=null)
		{
			Vector3 velo = Vector3.Normalize(focus.transform.position-gameObject.transform.position)*velocity;
			gameObject.rigidbody.velocity = velo;
			if((Mathf.Abs(focus.transform.position.x-gameObject.transform.position.x)<0.25f) && (Mathf.Abs(focus.transform.position.y-gameObject.transform.position.y)<0.25f))
			{
				ResolveCollision();
			}
		}
	}
	
	void ResolveCollision()
	{
		int value = (int)(focus.GetComponent<EnemyShip>().GetType().GetProperty(weap.PropertyToHit).GetValue(focus.GetComponent<EnemyShip>(), null));
		focus.GetComponent<EnemyShip>().GetType().GetProperty(weap.PropertyToHit).SetValue(focus.GetComponent<EnemyShip>(),value + weap.Power, null);
		Destroy(gameObject);
	}
}
