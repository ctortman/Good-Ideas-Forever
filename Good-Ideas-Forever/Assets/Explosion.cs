using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{
	public Texture[] frames;
	public int framesPerSecond = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
//		int index = ((int)( Time.time) * (framesPerSecond )) % frames.Length;
//		renderer.material.mainTexture = frames[index];
	}
}
