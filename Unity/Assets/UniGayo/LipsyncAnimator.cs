//Copyright (c) 2014 Rumpus Animation
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
//to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
//and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//		
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//		
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unigayo;

namespace Unigayo
{
	/// <summary>
	/// Base class for lipsync animation componets. Create a child class of this and override the OnPhonetic method if you need it.
	/// </summary>
	public abstract class LipsyncAnimator : MonoBehaviour 
	{		
		const string restPhonetic = "Rest";
		const int framerate = 24;
		
		bool _isPlaying = false;	
		public bool isPlaying
		{
			get {return _isPlaying;}
			private set{_isPlaying = value;}
		}
		
		string lastMood = "";
		string lastPhonetic = "";
		
		/// <summary>
		/// Change what mood is currently being used to get the phonemes from. Can be used to change mood mid sequence
		/// </summary>
		/// <param name="mood">Mood.</param>
		public void SetMood(string mood)
		{
			if(isPlaying)
			{
				lastMood = mood;
				OnPhonetic(mood, lastPhonetic);
			}
		}
		
		/// <summary>
		/// Play the specified lipsyncSequence, duration for the animation is passed in here as well along with the mood
		/// </summary>
		/// <param name="lipsyncSequence">Lipsync sequence.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="mood">Mood.</param>
		public void Play(LipsyncSequence lipsyncSequence, float duration, string mood)
		{
			Stop();
			StartCoroutine( Animate(lipsyncSequence, duration, mood) );
		}
		
		/// <summary>
		/// Stops playback and resets the sprite to the current mood's rest phoneme
		/// </summary>
		public void Stop()
		{
			StopAllCoroutines();
			isPlaying = false;
			
			if(!string.IsNullOrEmpty(lastMood))
				OnPhonetic(lastMood, restPhonetic);
		}
		
		/// <summary>
		/// Do the actual animation.
		/// </summary>
		/// <param name="sequence">Sequence.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="mood">Mood.</param>
		protected IEnumerator Animate(LipsyncSequence sequence, float duration, string mood)
		{
			isPlaying = true;
			
			float frameTime = 1;			
			int phonomereCount = 0;
			
			lastMood = mood;
												
			while(true)
			{
				frameTime += framerate * Time.deltaTime;
				
				if(sequence.frames[phonomereCount].frameTime <= frameTime)
				{
					lastPhonetic = sequence.frames[phonomereCount].phonetic;
					
					OnPhonetic(lastMood, lastPhonetic);
					phonomereCount++;
				}				
				
				if(phonomereCount >= sequence.frames.Count)
					break;
				
				yield return 0;
			}
			
			yield return new WaitForSeconds( ( duration * framerate - frameTime ) / framerate);
			
			OnPhonetic(mood, restPhonetic);
			
			lastMood = "";
			lastPhonetic = "";		
			
			isPlaying = false;
		}
		
		/// <summary>
		/// Event called when a new phonetic is hit in the sequence
		/// </summary>
		/// <param name="mood">Mood.</param>
		/// <param name="phonetic">Phonetic.</param>
		protected abstract void OnPhonetic(string mood, string phonetic);
	}
}