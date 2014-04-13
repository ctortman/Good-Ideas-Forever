using UnityEngine;
using System.Collections;
public class MenuSceneManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// we check what music we were playing and fade it out (we may go from Intro, 
		// Scores or Game scene, to Menu)
		// For testing purposes, we check all scenes 
		if (BaseManager.globalIntroMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalIntroMusic, -0.05f));
		}
		if (BaseManager.globalScoresMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalScoresMusic, -0.25f));
		}
		if (BaseManager.globalGameMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalGameMusic, -0.25f));
		}
		if (BaseManager.globalEndMusic != null)
		{
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalEndMusic, -0.25f));
		}
		// we make sure we have a null clip to begin with
		if (BaseManager.globalMenuMusic == null)
		{
			// create and return the Intro Scene music audio
			BaseManager.globalMenuMusic = AudioHelper.CreateGetFadeAudioObject 
				(BaseManager.instance.menuMusic, true, BaseManager.instance.fadeClip, "Audio-MenuMusic");
			// play the clip
			StartCoroutine (AudioHelper.FadeAudioObject (BaseManager.globalMenuMusic, 0.25f));
		}
	}
}