using UnityEngine;
using System.Collections;
// Is the parent Base manager all other Managers will use AudioClips or other data from

public class BaseManager : MonoBehaviour 
{
	// instance of class (Singleton)
	public static BaseManager instance = null;
	// audio clips to play looped and fade in/out 
	public AudioClip introMusic;
	public static GameObject globalIntroMusic;
	public AudioClip menuMusic;
	public static GameObject globalMenuMusic;
	public AudioClip gameMusic;
	public static GameObject globalGameMusic;
	public AudioClip loseMusic;
	public static GameObject globalLoseMusic;
	public AudioClip winMusic;
	public static GameObject globalWinMusic;
	// our fade animation clip
	public AnimationClip fadeClip;
	// audio clips to play one time
	public AudioClip sfx1;
	public AudioClip sfx2;
	public AudioClip sfx3;
	//public Texture2D backgroundSplash;


	// declare instance
	void OnEnable()
	{
		if (instance == null) 
		{
			instance = this;
			DontDestroyOnLoad (this);
		}
	}
	// demonstration buttons for switching scenes
	void OnGUI()
	{
		// display an area for buttons
		//75,75,400,400,     default 20,20,600,600
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		//GUI.BeginGroup (new Rect (0, 0, 300, 198));
		//GUI.Box (new Rect (0, 0, 300, 198), backgroundSplash);
		//GUILayout.Label ("Load a scene and fade in a looped Audio Clip");
		GUILayout.Space (Screen.height*2/3);
		//if (GUILayout.Button ("Menu Scene", GUILayout.Height (50))) { Application.LoadLevel 
		//    (1); }
		if (GUILayout.Button ("Game Scene", GUILayout.Height (Screen.height/10))) { Application.LoadLevel 
			(2); }
		//if (GUILayout.Button ("Title", GUILayout.Height (50))) {Application.LoadLevel 
		//	(0); }
		//if (GUILayout.Button ("End Scene", GUILayout.Height (60))) { Application.LoadLevel 
		//	(4); }
		GUILayout.Space (Screen.height/20);
		// title
		//GUILayout.Label ("Play a single sound");
		GUILayout.BeginHorizontal ();
		// Demonstrates playing a single sound clip
		if (GUILayout.Button ("Play SFX 1")) { AudioHelper.CreatePlayAudioObject 
			(sfx1); }
		if (GUILayout.Button ("Play SFX 2")) { AudioHelper.CreatePlayAudioObject 
			(sfx2); }
		if (GUILayout.Button ("Play SFX 3")) { AudioHelper.CreatePlayAudioObject 
			(sfx3); }
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}
}