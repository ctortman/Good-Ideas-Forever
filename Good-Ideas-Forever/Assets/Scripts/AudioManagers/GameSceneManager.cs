//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class GameSceneManager : MonoBehaviour 
//{
//	// Use this for initialization
//	void Start () 
//	{
//		// we initialize and start a fade sound like so
//		// 1. create and return the Intro Scene music audio
//		// this doesn't play the sound, but it provides a gameobject and a handle to it
//		BaseManager.globalGameMusic = AudioHelper.CreateGetFadeAudioObject 
//			(BaseManager.instance.gameMusic, true, BaseManager.instance.fadeClip, "Audio-GameMusic");
//		// 2. play the clip with fade in
//		StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalGameMusic, 0.5f));
//	}
//}

using UnityEngine;
using System.Collections;
public class GameSceneManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// we check what music we were playing and fade it out (we may go from Intro, 
		// Scores or Game scene, to Menu)
		// For testing purposes, we check all scenes 
		if (BaseManager.globalMenuMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalMenuMusic, -0.8f));
		}
		if (BaseManager.globalLoseMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalLoseMusic, -2.8f));
		}
		if (BaseManager.globalIntroMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalIntroMusic, -0.8f));
		}
		if (BaseManager.globalWinMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalWinMusic, -2.8f));
		}
		// we make sure we have a null clip to begin with
		if (BaseManager.globalGameMusic == null)
		{
			// create and return the Intro Scene music audio
			BaseManager.globalGameMusic = AudioHelper.CreateGetFadeAudioObject 
				(BaseManager.instance.gameMusic, true, BaseManager.instance.fadeClip, "Audio-GameMusic");
			// play the clip
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalGameMusic, 0.8f));
		}
	}
}
