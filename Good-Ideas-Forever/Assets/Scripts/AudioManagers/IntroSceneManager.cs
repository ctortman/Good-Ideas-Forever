using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroSceneManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// we initialize and start a fade sound like so
		// 1. create and return the Intro Scene music audio
		// this doesn't play the sound, but it provides a gameobject and a handle to it
		BaseManager.globalIntroMusic = AudioHelper.CreateGetFadeAudioObject 
		 (BaseManager.instance.introMusic, true, BaseManager.instance.fadeClip, "Audio-IntroMusic");
		// 2. play the clip with fade in
		StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalIntroMusic, 0.1f));
	}
}