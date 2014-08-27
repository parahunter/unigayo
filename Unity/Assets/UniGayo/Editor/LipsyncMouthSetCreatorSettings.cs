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
	/// Asset that stores the import settings for creating a mouth set
	/// </summary>
	[System.Serializable]
	public class LipsyncMouthSetCreatorSettings : ScriptableObject 
	{
		public string exportFolderPath;
		public string setName;
		
		public List<int> spriteAssignment;
		
		[System.Serializable]
		public class ImportMouth
		{
			public string mood;
			public Sprite spritesheet; //this is actually a sprite sheet
		}
	
		public List<ImportMouth> mouths;
		
		public string filePath;
		
		private int[] standardAssignment = {0,1,2,3,4,5,6,7,8,9};
		
		
		void OnEnable()
		{
			if(spriteAssignment == null)
			{	
				spriteAssignment = new List<int>();
				spriteAssignment.AddRange( standardAssignment );
				
				mouths = new List<ImportMouth>();
			}
		}
	}
}