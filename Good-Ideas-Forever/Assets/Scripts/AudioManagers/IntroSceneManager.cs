//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class IntroSceneManager : MonoBehaviour 
//{
//	// Use this for initialization
//	void Start () 
//	{
//		// we initialize and start a fade sound like so
//		// 1. create and return the Intro Scene music audio
//		// this doesn't play the sound, but it provides a gameobject and a handle to it
//		BaseManager.globalIntroMusic = AudioHelper.CreateGetFadeAudioObject 
//		 (BaseManager.instance.introMusic, true, BaseManager.instance.fadeClip, "Audio-IntroMusic");
//		// 2. play the clip with fade in
//		StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalIntroMusic, 0.5f));
//	}
//}

using UnityEngine;
using System.Collections;
public class IntroSceneManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// we check what music we were playing and fade it out (we may go from Intro, 
		// Scores or Game scene, to Menu)
		// For testing purposes, we check all scenes 
		if (BaseManager.globalMenuMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalMenuMusic, -2.0f));
		}
		if (BaseManager.globalLoseMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalLoseMusic, -4.0f));
		}
		if (BaseManager.globalGameMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalGameMusic, -2.0f));
		}
		if (BaseManager.globalWinMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalWinMusic, -4.0f));
		}
		// we make sure we have a null clip to begin with
		if (BaseManager.globalIntroMusic == null)
		{
			// create and return the Intro Scene music audio
			BaseManager.globalIntroMusic = AudioHelper.CreateGetFadeAudioObject 
				(BaseManager.instance.introMusic, true, BaseManager.instance.fadeClip, "Audio-IntroMusic");
			// play the clip
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalIntroMusic, 0.5f));
		}
	}
}






