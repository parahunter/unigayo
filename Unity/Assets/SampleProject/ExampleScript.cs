using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unigayo;

public class ExampleScript : MonoBehaviour 
{
	public LipsyncAnimator lipsyncAnimator;
	public AudioClip[] clipsToPlay;
	public LipsyncSequenceDatabase lipsyncDatabase;
	
	void OnGUI()
	{
		if(	GUILayout.Button("Clip 1"))
			Play(clipsToPlay[0]);
		if(	GUILayout.Button("Clip 2"))
			Play(clipsToPlay[1]);
			
		if(lipsyncAnimator.isPlaying)
		{
			if( GUILayout.Button("Stop") )
			{
				audio.Stop();
				lipsyncAnimator.Stop();
			}
		}
	}
	
	void Play(AudioClip clip)
	{
		audio.clip = clip;
		audio.Play();
		
		lipsyncAnimator.Play( lipsyncDatabase.GetSequence(clip.name), clip.length, "Happy");
        
    }
}
