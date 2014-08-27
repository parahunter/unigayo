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
	public class LipsyncMouthSet : ScriptableObject
	{
		public List<LipsyncPrestonBlairMouth> mouths = new List<LipsyncPrestonBlairMouth>();
		
		public bool HasMood(string mood)
		{
			LipsyncPrestonBlairMouth mouth = mouths.Find( m => m.mood == mood);
			
			return mouth == null ? false : true;
		}
		
		public Sprite GetSprite(string mood, string phonetic)
		{
			LipsyncPrestonBlairMouth mouth = mouths.Find( m => m.mood == mood);
			
			if(mouth == null)
			{
				Debug.LogWarning("Could not found mouth for " + mood + " trying to default to first mouth");
				if(mouths.Count > 0)
				{
					mouth = mouths[0];
				}
				else
				{
					Debug.LogError("No mouths assigned!");
				}
				
			}
			
			Sprite sprite = mouth.rest;
			switch(phonetic)
			{
			case "AI":
				sprite = mouth.AI;
					break;	
			case "E":
				sprite = mouth.E;
				break;
			case "etc":
				sprite = mouth.etc;
				break;
			case "FV":
				sprite = mouth.FV;
				break;
			case "L":
				sprite = mouth.L;
				break;
			case "MBP":
				sprite = mouth.MBP;
				break;
			case "O":
				sprite = mouth.O;
				break;
			case "U":
				sprite = mouth.U;
				break;	
			case "WQ":
				sprite = mouth.WQ;
				break;	
			}
					
			return sprite;						
		}
	}
}