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
using UnityEditor;
using Unigayo;
using System.Linq;
using System;

namespace Unigayo
{
	public static class LipsyncMouthSetFactory 
	{
		public static bool CreateSet(LipsyncMouthSetCreatorSettings.ImportMouth[] importMouths, int[] spriteAssignment, out LipsyncMouthSet set)
		{		
			set = ScriptableObject.CreateInstance<LipsyncMouthSet>();
							
			for(int i = 0 ; i < importMouths.Length ; i++)
			{
				LipsyncPrestonBlairMouth mouth = new LipsyncPrestonBlairMouth();
							
				mouth.mood = importMouths[i].mood;
				
				Sprite[] spriteSet = GetSpriteSet(importMouths[i].spritesheet);
							
				for(int s = 0 ; s < spriteAssignment.Length ; s++)
				{	
					try
					{
						mouth[s] = spriteSet[ spriteAssignment[s] ];
					}
					catch (IndexOutOfRangeException e)
					{
						Debug.Log("sprite assignment failed " + importMouths[i].spritesheet);
					}	
				}
							
				set.mouths.Add( mouth );
			}
					
			return true;
		}
		
		static Sprite[] GetSpriteSet(Sprite spritesheet)
		{
			string path = AssetDatabase.GetAssetPath(spritesheet);
			
			UnityEngine.Object[] sortedObjects = AssetDatabase.LoadAllAssetsAtPath(path).Where( o => o.GetType() == typeof(Sprite)).ToArray();
			
			List<Sprite> allSprites = new List<Sprite>();
			foreach(UnityEngine.Object obj in sortedObjects)
			{
				allSprites.Add( (Sprite) obj );
			}
			
			return allSprites.ToArray();
		}
	}
}