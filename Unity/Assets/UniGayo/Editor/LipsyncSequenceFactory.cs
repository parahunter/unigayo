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
	/// Factory class to creat LipsyncSequence assets
	/// </summary>
	public static class LipsyncSequenceFactory 
	{
		public static LipsyncSequence BuildSequence(string[] lines, string name, string mood = "Neutral")
		{
			return Parse(lines, name, mood);
		}
		
		private static LipsyncSequence Parse(string[] lines, string name, string mood)
		{
			LipsyncSequence sequence = new LipsyncSequence();
			sequence.mood = mood;
			sequence.name = name;
			
			foreach(string line in lines)
			{
				string[] tokens = line.Split(' ');
				
				int frameNumber;
				if(int.TryParse( tokens[0] , out frameNumber))
				{
					sequence.frames.Add( new LipsyncFrame(frameNumber, tokens[1]) );
				}
			}
			
			return sequence;		
		}
	}
}