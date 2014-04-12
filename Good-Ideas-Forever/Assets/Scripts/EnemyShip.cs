using UnityEngine;
using System.Collections;

public class EnemyShip : Ship {

	// Use this for initialization
	void Start () {
		this.Health = 3;
		this.Peace = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public NinjaForce Side 
	{
		get; 
		set;
	}
	public int Health 
	{
		get;
		set;
	}
	public int Peace 
	{
		get;
		set;
	}

		public int getGoal (){
				//GetGoal will return -1, 0 or 1 depending on the movement that should be made by the AI
				bool above = true;
				bool below = true;
				EnemyShip target1 = new EnemyShip ();
				EnemyShip target2 = new EnemyShip ();
				//Step 1: Check if there are ships above
				if (GameState.instance.DoesSpaceContainObject (this.StartX, this.StartY - 1)) {
						above = false;
				}
				if (GameState.instance.DoesSpaceContainObject (this.StartX, this.StartY + 1)) {
						below = false;
				}

				if(above){
						target1 = getClosest (1);
				}
				else if(below){
						target2 = getClosest (-1);
				}
				else{
						//If neither above or below were true, then we can't move
						return 0;
				}

				if(target1 == target2){
						//Ships are the same. This can only be if they're on our current row
						return 0;
				}
				else{
						int north = target1.StartY;
						int south = target2.StartY;
						if (north < south) {
								//North is closer
								return -1;
						} 
						else if (south < north) {
								//South is closer
								return 1;
						}
						else{
								//North = South
								System.Random r = new System.Random();
								int nextValue = r.Next(1, 100);
								if(nextValue > 49){
										return -1;
								}
								else{
										return 1;
								}
						}
								
				}
		}
		public EnemyShip getClosest(int direction){
				EnemyShip[] targets = this.CurrentWeapon.GetTargets(this.StartX, this.StartY);
				if(targets.Length > 0){
						//We have enemies in our row!
						return targets [0];
				}
				else{
						//Given direction, we're either checking up or down
						if(direction > 0){
								//Going Up!
								for(int myY = StartY; myY >= 0; myY--){
										targets = this.CurrentWeapon.GetTargets(this.StartX, this.StartY);
										if(targets.Length > 0){
												//We have enemies in the current row!
												return targets [0];
										}
								}
								//Found no targets in the Up direction
								return null;
						}
						else if (direction < 0){
								//Going Down!
								for(int myY = StartY; myY < GameState.instance.BoardHeight; myY++){
										targets = this.CurrentWeapon.GetTargets(this.StartX, this.StartY);
										if(targets.Length > 0){
												//We have enemies in the current row!
												return targets [0];
										}
								}
								//Found no targets in the down direction
								return null;
						}
						else{
								//Direction is 0, so we can't move.  We've already checked if there was a target in a row and there wasn't.  NULL!
								return null;
						}

				}
		}

}
public enum NinjaForce
{
	Left,
	Right
}
